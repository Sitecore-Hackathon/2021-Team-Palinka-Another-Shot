namespace Feature.Workbox.Services
{
    using Feature.Workbox.Interfaces;
    using Feature.Workbox.Models.Response;
    using Feature.Workbox.Models.Response.Response;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.SecurityModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class WorkflowRepository.
    /// Implements the <see cref="Feature.Workbox.Interfaces.IWorkflowRepository" />
    /// </summary>
    /// <seealso cref="Feature.Workbox.Interfaces.IWorkflowRepository" />
    public class WorkflowRepository : IWorkflowRepository
    {
        /// <summary>
        /// Gets the detailed workflow.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>DetailedWorkflow.</returns>
        public DetailedWorkflow GetDetailedWorkflow(string id)
        {
            var response = new DetailedWorkflow();
            var wfItem = Sitecore.Context.Database.GetItem(new ID(id));

            response.Id = wfItem.ID.ToString();
            response.Name = wfItem.Name;

            List<Item> items = new List<Item>();
            // TODO: REWRITE TO CONTENT SEARCH
            //"__Workflow", "__Workflow state"
            using (new SecurityDisabler())
            {
                using (new DatabaseSwitcher(Factory.GetDatabase(Constants.Databases.Master)))
                {
                    items = Sitecore.Context.Database.GetItem(new ID("{0DE95AE4-41AB-4D01-9EB0-67441B7C2450}")).Axes.GetDescendants().ToList();
                }
            }

            foreach (var wfState in wfItem.Children.Where(t => t.TemplateID == Templates.WorkflowState.TemplateId))
            {
                var state = new DetailedWorkflowState
                {
                    Id = wfState.ID.ToString(),
                    IsFinal = wfState[Templates.WorkflowState.Fields.Final] == "1",
                    Name = wfState.Name
                };

                state.Items = items.Where(t => t["__Workflow"].Equals(wfItem.ID.ToString()) && t["__Workflow State"].Equals(wfState.ID.ToString()))
                    .Select(item => new WorkflowItem
                    {
                        ID = item.ID.ToString(),
                        Name = item.Name,
                        Language = item.Language?.Name ?? string.Empty,
                        LastUpdated = item.Statistics?.Updated ?? DateTime.MinValue,
                        LastUpdatedBy = item.Statistics?.UpdatedBy ?? string.Empty,
                        TemplateName = item.TemplateName,
                        NextStates = GetNextStates(item, wfState)
                    }).ToList();

                response.States.Add(state);
            }

            response.QuickFilters.Add("TemplateName", response.States?.SelectMany(t => t.Items).Select(t => t.TemplateName).Distinct());
            response.QuickFilters.Add("LastUpdatedBy", response.States?.SelectMany(t => t.Items).Select(t => t.LastUpdatedBy).Distinct());

            return response;
        }

        /// <summary>
        /// Gets the workflows.
        /// </summary>
        /// <returns>List&lt;Workflow&gt;.</returns>
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
            foreach (var wfCommand in wfStateItem.Children.Where(t => t.TemplateID == Templates.WorkflowCommand.TemplateId))
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
            return result;
        }

        /// <summary>
        /// Gets the actions.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="commands">The commands.</param>
        /// <returns>List&lt;WorkboxAction&gt;.</returns>
        private List<WorkboxAction> GetActions(Item item, List<Item> commands)
        {
            var result = new List<WorkboxAction>();
            // TODO:  //Appearance Evaluator Type EVALUATE
            foreach (var wfCommand in commands)
            {
                result.Add(new WorkboxAction
                {
                    ID = wfCommand.ID.ToString(),
                    Name = wfCommand.Name,
                    SuppressComment = wfCommand[Templates.WorkflowCommand.Fields.SuppressComment] == "1"
                });
            }
            return result;
        }
    }
}