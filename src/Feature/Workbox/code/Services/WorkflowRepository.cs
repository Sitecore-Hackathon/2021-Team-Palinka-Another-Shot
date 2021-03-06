﻿namespace Feature.Workbox.Services
{
    using Feature.Workbox.Interfaces;
    using Feature.Workbox.Models.Memory;
    using Feature.Workbox.Models.Response;
    using Feature.Workbox.Models.Response.Response;
    using Feature.Workbox.Models.Search;
    using Sitecore.Configuration;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.Security;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Data.Managers;
    using Sitecore.Globalization;
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

        /// <summary>
        /// The sitecore factory
        /// </summary>
        private readonly ISitecoreFactory _sitecoreFactory;

        /// <summary>
        /// The master database
        /// </summary>
        private readonly Database _masterDatabase;

        /// <summary>
        /// The items in wrong state
        /// </summary>
        private List<MemoryStoreItem> itemsInWrongState = new List<MemoryStoreItem>();

        public WorkflowRepository(IWorkflowLogger logger, ISitecoreFactory sitecoreFactory)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._sitecoreFactory = sitecoreFactory ?? throw new ArgumentNullException(nameof(sitecoreFactory));
            this._masterDatabase = this._sitecoreFactory.GetDatabase(Constants.Databases.Master);
        }

        /// <summary>
        /// Gets the detailed workflow view with items
        /// </summary>
        /// <param name="id">The workflow identifier.</param>
        /// <returns>DetailedWorkflow view with items.</returns>
        public DetailedWorkflow GetDetailedWorkflow(string id)
        {
            var response = new DetailedWorkflow();
            var wfItem = this._masterDatabase.GetItem(new ID(id));

            response.Id = wfItem.ID.ToString();
            response.Name = wfItem.Name;

            List<Item> items = new List<Item>();

            var index = ContentSearchManager.GetIndex(Constants.IndexNames.SitecoreMasterIndex);
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
                        var wfStateId = Sitecore.ContentSearch.Utilities.IdHelper.NormalizeGuid(wfState.ID);
                        var resultItems = context.GetQueryable<WorkflowSearchItem>().Where(p => p.WorkflowState == wfStateId && p.IsLatestVersion);
                        var searchResultItems = resultItems.GetResults();

                        foreach (var resultItem in searchResultItems.Hits)
                        {
                            var item = this._masterDatabase.GetItem(resultItem.Document.ItemId, Language.Parse(resultItem.Document.Language));

                            if (item != null)
                            {
                                var workflowItem = new WorkflowItem()
                                {
                                    ID = item.ID.ToString(),
                                    Name = item.Name,
                                    Language = resultItem.Document.Language ?? string.Empty,
                                    LastUpdated = item.Statistics?.Updated ?? DateTime.MinValue,
                                    LastUpdatedBy = item.Statistics?.UpdatedBy ?? string.Empty,
                                    TemplateName = item.TemplateName,
                                    HasLayout = item.Visualization?.Layout != null,
                                    CurrentVersion = item.Version.Number,
                                    Icon = GetIconUrl(item),
                                    NextStates = GetNextStates(item, wfState)
                                };

                                var currentDbState = item[Constants.StandardFieldNames.WorkflowState];
                                if (currentDbState.Equals(wfState.ID.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    state.Items.Add(workflowItem);
                                }
                                else
                                {
                                    // State is not matching in the index and in the db, maybe due to the async indexing operations
                                    // We still need to ensure the proper state
                                    var previousState = response.States.FirstOrDefault(t => t.Id.Equals(currentDbState));
                                    if (previousState != null)
                                    {
                                        // Item state should point to a previous state
                                        previousState.Items.Add(new WorkflowItem()
                                        {
                                            ID = item.ID.ToString(),
                                            Name = item.Name,
                                            Language = resultItem.Document.Language ?? string.Empty,
                                            LastUpdated = item.Statistics?.Updated ?? DateTime.MinValue,
                                            LastUpdatedBy = item.Statistics?.UpdatedBy ?? string.Empty,
                                            TemplateName = item.TemplateName,
                                            HasLayout = item.Visualization?.Layout != null,
                                            CurrentVersion = item.Version.Number,
                                            Icon = GetIconUrl(item),
                                            NextStates = GetNextStates(item, this.GetItem(currentDbState))
                                        }) ;
                                    }
                                    else
                                    {
                                        // Item 
                                        itemsInWrongState.Add(new MemoryStoreItem
                                        {
                                            ItemId = item.ID.ToString(),
                                            State = currentDbState,
                                            Language = item.Language.Name
                                        });
                                    }
                                }
                                
                            }
                        }

                        foreach (var itemInWrongState in itemsInWrongState.Where(t => t.State.Equals(state.Id)))
                        {
                            var item = this._masterDatabase.GetItem(itemInWrongState.ItemId, Language.Parse(itemInWrongState.Language));

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
                                    HasLayout = item.Visualization?.Layout != null,
                                    CurrentVersion = item.Version.Number,
                                    Icon = GetIconUrl(item),
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
        /// Gets the item by id.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns>The item.</returns>
        public Item GetItem(string id, string language)
        {
            return this._masterDatabase.GetItem(new ID(id), Language.Parse(language));
        }

        /// <summary>
        /// Gets the item by id.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns>The item.</returns>
        public Item GetItem(string id)
        {
            return this._masterDatabase.GetItem(new ID(id), Language.Parse(Constants.Languages.En));
        }

        /// <summary>
        /// Gets the workflow history for an Item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>List of workflow history events</returns>
        public Sitecore.Workflows.WorkflowEvent[] GetHistory(Item item)
        {
            return this._masterDatabase.WorkflowProvider.GetWorkflow(item).GetHistory(item);
        }

        /// <summary>
        /// Gets the all workflows.
        /// </summary>
        /// <returns>List all available workflows in the system</returns>
        public List<Workflow> GetWorkflows()
        {
            var result = new List<Workflow>();
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
                            IsSelected = result.Count() == 0
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
                            Name = _masterDatabase.GetItem(new ID(wfCommand[Templates.WorkflowCommand.Fields.NextState])).Name
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
        /// Gets the workflow.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The workflow for item</returns>
        public Sitecore.Workflows.IWorkflow GetWorkflow(Item item)
        {
            return this._masterDatabase.WorkflowProvider.GetWorkflow(item);
        }



        /// <summary>
        /// Gets the icon URL.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The icon url.</returns>
        public string GetIconUrl(Item item)
        {
            if (item == null)
            {
                return "";
            }
            string iconImageRaw = ThemeManager.GetIconImage(item, 32, 32, "", "");
            if (!string.IsNullOrWhiteSpace(iconImageRaw) && iconImageRaw.Contains("src="))
            {
                int i0 = iconImageRaw.IndexOf("src=");
                int i1 = iconImageRaw.IndexOf('"', i0 + 1);
                if (i1 < 0)
                {
                    return null;
                }

                int i2 = iconImageRaw.IndexOf('"', i1 + 1);
                if (i2 < 0)
                {
                    return null;
                }

                return iconImageRaw.Substring(i1, i2 - i1).Trim(' ', '"', '\\');
            }

            return null;
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