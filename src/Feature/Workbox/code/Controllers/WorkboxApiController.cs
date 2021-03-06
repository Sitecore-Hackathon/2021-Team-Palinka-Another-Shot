namespace Feature.Workbox.Controllers
{
    using Feature.Workbox.Interfaces;
    using Feature.Workbox.Models.Request;
    using Feature.Workbox.Models.Response;
    using Feature.Workbox.Models.Response.Response;
    using Sitecore.Services.Infrastructure.Web.Http;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;
    
    /// <summary> Redesignedworkbox api controller data
    /// Implements the <see cref="Sitecore.Services.Infrastructure.Web.Http.ServicesApiController" /></summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[Authorize]
    public class WorkboxApiController : ServicesApiController
    {
        /// <summary>
        /// The workflow service
        /// </summary>
        private readonly IWorkflowService _workflowService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Feature.Workbox.Controllers.WorkboxApiController" /> class.
        /// </summary>
        /// <param name="workflowService">The workflow service.</param>
        public WorkboxApiController(IWorkflowService workflowService)
        {
            this._workflowService = workflowService;
        }

        /// <summary>
        /// Retrieves the available workflows from Sitecore
        /// </summary>
        /// <returns>List of available workflows</returns>
        [HttpGet]
        public List<Workflow> Workflows()
        {
            return this._workflowService.GetWorkflows();
        }

        /// <summary>
        /// Returns the detailed view of the given workflow byid
        /// </summary>
        /// <param name="id">The workflow identifier.</param>
        /// <returns>Detailed workflow with items.</returns>
        [HttpGet]
        public DetailedWorkflow Detail(string id)
        {
            return this._workflowService.GetDetailedWorkflow(id);
        }

        /// <summary>
        /// Items the detail.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns>The itemdetails view.</returns>
        [HttpGet]
        public ItemDetailsResponse ItemDetail(string itemId, string language)
        {
            return this._workflowService.GetItemDetails(itemId, language);
        }

        /// <summary>
        /// Changes the workflow state on the given item
        /// </summary>
        /// <param name="request">The request dto.</param>
        /// <returns>The response dto.</returns>
        [HttpPost]
        public ChangeWorkflowResponse ChangeWorkflow(ChangeWorkflowRequest request)
        {
            return this._workflowService.ChangeWorkflow(request);
        }
    }
}