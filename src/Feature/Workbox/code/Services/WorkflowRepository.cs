namespace Feature.Workbox.Services
{
    using Feature.Workbox.Interfaces;
    using Feature.Workbox.Models.Response;
    using Feature.Workbox.Models.Response.Response;
    using Feature.Workbox.Models.Search;
    using Sitecore.Configuration;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.Security;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.SecurityModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Class WorkflowRepository.
    /// Implements the <see cref="Feature.Workbox.Interfaces.IWorkflowRepository" />
    /// </summary>
    /// <seealso cref="Feature.Workbox.Interfaces.IWorkflowRepository" />
    public class WorkflowRepository : IWorkflowRepository
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly IWorkflowLogger _logger;

        public WorkflowRepository(IWorkflowLogger logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets the detailed workflow view with items
        /// </summary>
        /// <param name="id">The workflow identifier.</param>
        /// <returns>DetailedWorkflow view with items.</returns>
        public DetailedWorkflow GetDetailedWorkflow(string id)
        {
            var response = new DetailedWorkflow();
            var wfItem = Sitecore.Context.Database.GetItem(new ID(id));

            response.Id = wfItem.ID.ToString();
            response.Name = wfItem.Name;

            List<Item> items = new List<Item>();

            var index = ContentSearchManager.GetIndex("sitecore_master_index");
            if (index != null)
            {
                foreach (var wfState in wfItem.Children.Where(t => t.TemplateID == Templates.WorkflowState.TemplateId))
                {
                    var state = new DetailedWorkflowState
                    {
                        Id = wfState.ID.ToString(),
                        IsFinal = wfState[Templates.WorkflowState.Fields.Final] == "1",
                        Name = wfState.Name
                    };

                    using (var context = index.CreateSearchContext(SearchSecurityOptions.DisableSecurityCheck))
                    {
                        var resultItems = context.GetQueryable<WorkflowSearchItem>().Where(p => p.WorkflowID == wfItem.ID.ToString() && p.WorkflowState == wfState.ID.ToString() && p.IsLatestVersion);
                        var searchResultItems = resultItems.GetResults();

                        foreach (var resultItem in searchResultItems.Hits)
                        {
                            var item = Factory.GetDatabase("master").GetItem(new ID(resultItem.Document.ID));

                            if (item != null)
                            {
                                var workflowItem = new WorkflowItem()
                                {
                                    ID = item.ID.ToString(),
                                    Name = item.Name,
                                    Language = item.Language?.Name ?? string.Empty,
                                    LastUpdated = item.Statistics?.Updated ?? DateTime.MinValue,
                                    LastUpdatedBy = item.Statistics?.UpdatedBy ?? string.Empty,
                                    TemplateName = item.TemplateName,
                                    NextStates = GetNextStates(item, wfState)
                                };

                                state.Items.Add(workflowItem);
                            }
                        }

                        response.States.Add(state);
                    }
                }
            }

            response.QuickFilters.Add("TemplateName", response.States?.SelectMany(t => t.Items).Select(t => t.TemplateName).Distinct());
            response.QuickFilters.Add("LastUpdatedBy", response.States?.SelectMany(t => t.Items).Select(t => t.LastUpdatedBy).Distinct());

            return response;
        }

        /// <summary>
        /// Gets the all workflows.
        /// </summary>
        /// <returns>List all available workflows in the system</returns>
        public List<Workflow> GetWorkflows()
        {
            var result = new List<Workflow>();
            int i = 0;
            using (new SecurityDisabler())
            {
                using (new DatabaseSwitcher(Factory.GetDatabase(Constants.Databases.Master)))
                {
                    var wfRootItem = Sitecore.Context.Database.GetItem(new ID(Constants.ItemIds.WorkflowRootFolderId));

                    foreach (var wfItem in wfRootItem.Children.Where(t => t.TemplateID == Templates.Workflow.TemplateId))
                    {
                        var workflow = new Workflow
                        {
                            Id = wfItem.ID.ToString(),
                            Name = wfItem.Name,
                            IsSelected = i == 0 // TODO: Change it
                        };

                        foreach (var wfState in wfItem.Children.Where(t => t.TemplateID == Templates.WorkflowState.TemplateId))
                        {
                            var state = new WorkflowState
                            {
                                Id = wfState.ID.ToString(),
                                IsFinal = wfState[Templates.WorkflowState.Fields.Final] == "1",
                                Name = wfState.Name
                            };

                            workflow.States.Add(state);
                        }

                        result.Add(workflow);
                        i++;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the next states.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="wfStateItem">The wf state item.</param>
        /// <returns>List&lt;NextWorkflowState&gt;.</returns>
        private List<NextWorkflowState> GetNextStates(Item item, Item wfStateItem)
        {
            var result = new List<NextWorkflowState>();
            var wfCommands = wfStateItem.Children.Where(t => t.TemplateID == Templates.WorkflowCommand.TemplateId);
            foreach (var wfCommand in wfCommands)
            {
                var appearance = wfCommand[Templates.WorkflowCommand.Fields.AppearanceEvaluatorType];
                if (ValidateAppearance(item, wfCommand, appearance))
                {
                    var nextState = result.FirstOrDefault(testc => testc.Id == wfCommand[Templates.WorkflowCommand.Fields.NextState]);

                    if (nextState == null)
                    {
                        nextState = new NextWorkflowState
                        {
                            Id = wfCommand[Templates.WorkflowCommand.Fields.NextState],
                            Name = Sitecore.Context.Database.GetItem(new ID(wfCommand[Templates.WorkflowCommand.Fields.NextState])).Name
                        };
                        result.Add(nextState);
                    }

                    nextState.Actions.Add(new WorkboxAction
                    {
                        ID = wfCommand.ID.ToString(),
                        Name = wfCommand.Name,
                        SuppressComment = wfCommand[Templates.WorkflowCommand.Fields.SuppressComment] == "1"
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// Validates the appearance of the given Workflow command
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="workflowCommand">The workflow command.</param>
        /// <param name="validatorType">Type of the validator.</param>
        /// <returns><c>true</c> if the command should be visible, <c>false</c> otherwise.</returns>
        private bool ValidateAppearance(Item item, Item workflowCommand, string validatorType)
        {
            if (string.IsNullOrEmpty(validatorType))
            {
                return true;
            }

            string assemblyName = string.Empty;
            string typeName = string.Empty;

            try
            {
                assemblyName = validatorType.Split(',')[1].Trim();
                typeName = validatorType.Split(',')[0].Trim();
            }
            catch (Exception ex)
            {
                this._logger.LogError(string.Format("{0} could not be parsed, use the following format: <NameSpace>.<Class>,<AssemblyName>", validatorType), ex);

                return false;
            }

            Assembly assembly = null;
            try
            {
                assembly = Assembly.Load(assemblyName);
            }
            catch (Exception ex)
            {
                this._logger.LogError(string.Format("{0} assembly could not be loaded, please check the configuration!", assemblyName), ex);
                return false;
            }

            Type type = assembly.GetType(typeName);

            if (type == null)
            {
                this._logger.LogError(string.Format("{0} type could not be loaded, please check the configuration!", typeName), null);
                return false;
            }

            var methodName = "EvaluateState";

            MethodInfo methodInfo = type.GetMethod(methodName);

            if (methodInfo == null)
            {
                this._logger.LogError(string.Format("{0} method could not be loaded, please make sure you implemented the 'DoHealthcheck' method!", methodName), null);

                return false;
            }

            Sitecore.Workflows.WorkflowCommandState result = Sitecore.Workflows.WorkflowCommandState.Hidden;
            ParameterInfo[] parameters = methodInfo.GetParameters();
            object classInstance = Activator.CreateInstance(type, null);

            object[] parametersArray = new object[] { item, workflowCommand };

            result = (Sitecore.Workflows.WorkflowCommandState)methodInfo.Invoke(classInstance, parametersArray);

            return result == Sitecore.Workflows.WorkflowCommandState.Visible;
        }
    }
}