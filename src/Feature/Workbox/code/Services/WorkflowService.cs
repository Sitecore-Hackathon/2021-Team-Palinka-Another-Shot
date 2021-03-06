namespace Feature.Workbox.Services
{
    using Feature.Workbox.Interfaces;
    using Feature.Workbox.Models.Request;
    using Feature.Workbox.Models.Response;
    using Feature.Workbox.Models.Response.Response;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class WorkflowService.
    /// Implements the <see cref="Feature.Workbox.Interfaces.IWorkflowService" />
    /// </summary>
    /// <seealso cref="Feature.Workbox.Interfaces.IWorkflowService" />
    public class WorkflowService : IWorkflowService
    {
        /// <summary>
        /// The sitecore factory
        /// </summary>
        private readonly ISitecoreFactory _sitecoreFactory;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly IWorkflowLogger _logger;

        /// <summary>
        /// The workflow repository
        /// </summary>
        private readonly IWorkflowRepository _workflowRepository;

        /// <summary>
        /// The master database
        /// </summary>
        private readonly Database _masterDatabase;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowService" /> class.
        /// </summary>
        /// <param name="sitecoreFactory">The sitecore factory.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="workflowRepository">The workflow repository.</param>
        /// <exception cref="ArgumentNullException">sitecoreFactory</exception>
        /// <exception cref="ArgumentNullException">logger</exception>
        public WorkflowService(ISitecoreFactory sitecoreFactory, IWorkflowLogger logger, IWorkflowRepository workflowRepository)
        {
            this._sitecoreFactory = sitecoreFactory ?? throw new ArgumentNullException(nameof(sitecoreFactory));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));

            this._masterDatabase = this._sitecoreFactory.GetDatabase(Constants.Databases.Master);
        }

        /// <summary>
        /// Changes the workflow state for the given item
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ChangeWorkflowResponse.</returns>
        public ChangeWorkflowResponse ChangeWorkflow(ChangeWorkflowRequest request)
        {
            var response = new ChangeWorkflowResponse
            {
                IsSuccess = false
            };

            var item = this._masterDatabase.GetItem(new ID(request.ItemId));

            Sitecore.Workflows.IWorkflow wf = this._masterDatabase.WorkflowProvider.GetWorkflow(item);

            if (wf != null)
            {
                try
                {
                    var result = wf.Execute(request.CommandId, item, request.Comment, false, new object[] { string.Empty });

                    response.IsSuccess = result.Succeeded;
                    response.Message = result.Message;
                }
                catch (Exception ex)
                {
                    response.Message = ex.Message;
                    this._logger.LogError(ex.Message, ex);
                }
            }

            return response;
        }

        /// <summary>
        /// Gets the detailed workflow view with items
        /// </summary>
        /// <param name="id">The workflow identifier.</param>
        /// <returns>DetailedWorkflow view with items.</returns>
        public DetailedWorkflow GetDetailedWorkflow(string id)
        {
            return this._workflowRepository.GetDetailedWorkflow(id);
        }

        /// <summary>
        /// Gets the item details.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns>The detailed item view.</returns>
        public ItemDetailsResponse GetItemDetails(string id, string language)
        {
            var response = new ItemDetailsResponse();

            var item = this._workflowRepository.GetItem(id, language);

            if (item == null)
            {
                response.IsSuccess = false;
                response.Message = $"Item with ID: {id} cannot be found";

                return response;
            }

            response.Name = item.Name;
            response.Id = item.ID.ToString();
            response.TemplateId = item.TemplateID.ToString();
            response.TemplateName = item.TemplateName;
            response.Language = item.Language.Name;
            response.Updated = item.Statistics.Updated;
            response.UpdatedBy = item.Statistics.UpdatedBy;
            response.Created = item.Statistics.Created;
            response.CreatedBy = item.Statistics.CreatedBy;
            response.FullPath = item.Paths.FullPath;

            var defaultDevice = this._sitecoreFactory.GetDefaultDevice();

            foreach(var rendering in item.Visualization.GetRenderings(defaultDevice, false))
            {
                if (!string.IsNullOrEmpty(rendering.Settings.PersonalizationTest))
                {
                    response.PersonalizedRenderings.Add(new PersonalizationDetailResponse
                    {
                        RenderingName = rendering.WebEditDisplayName
                    });
                }

                if (!string.IsNullOrEmpty(rendering.Settings.MultiVariateTest))
                {
                    response.MultiVariateTestedRenderings.Add(new MultiVariateTestDetailResponse
                    {
                        RenderingName = rendering.WebEditDisplayName
                    });
                }
            }

            var history = this._workflowRepository.GetHistory(item);

            response.History = history.Select(this.LoadHistoryRecord).ToList();
            response.IsSuccess = true;

            return response;
        }

        /// <summary>
        /// Gets the all workflows.
        /// </summary>
        /// <returns>List all available workflows in the system</returns>
        public List<Workflow> GetWorkflows()
        {
            return this._workflowRepository.GetWorkflows();
        }

        private WorkflowHistoryResponse LoadHistoryRecord(Sitecore.Workflows.WorkflowEvent item)
        {
            var historyRecord = new WorkflowHistoryResponse
            {
                Date = item.Date,
                CommentFields = item.CommentFields,
                NewState = item.NewState,
                OldState = item.OldState,
                User = item.User
            };

            if (!string.IsNullOrEmpty(historyRecord.OldState))
            {
                historyRecord.OldStateName = this._masterDatabase.GetItem(new ID(historyRecord.OldState))?.Name ?? string.Empty;
            }

            if (!string.IsNullOrEmpty(historyRecord.NewState))
            {
                historyRecord.NewStateName = this._masterDatabase.GetItem(new ID(historyRecord.NewState))?.Name ?? string.Empty;
            }

            return historyRecord;
        }
    }
}