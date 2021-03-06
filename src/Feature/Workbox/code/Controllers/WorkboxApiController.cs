namespace Feature.Workbox.Controllers
{
    using Feature.Workbox.Interfaces;
    using Feature.Workbox.Models.Response;
    using Feature.Workbox.Models.Response.Response;
    using Feature.Workbox.Services;
    using Sitecore.Services.Infrastructure.Web.Http;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;

    //[Authorize]
    /// <summary>Class WorkboxApiController.
    /// Implements the <see cref="Sitecore.Services.Infrastructure.Web.Http.ServicesApiController" /></summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WorkboxApiController : ServicesApiController
    {
        /// <summary>The workflow repository</summary>
        private readonly IWorkflowRepository _workflowRepository;

        /// <summary>Initializes a new instance of the <see cref="T:Feature.Workbox.Controllers.WorkboxApiController" /> class.</summary>
        public WorkboxApiController(IWorkflowRepository workflowRepository)
        {
            this._workflowRepository = workflowRepository;
        }

        /// <summary>
        /// Oks this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        [HttpGet]
        public string Ok()
        {
            return "Ok";
        }

        /// <summary>
        /// Workflowses this instance.
        /// </summary>
        /// <returns>List&lt;Workflow&gt;.</returns>
        [HttpGet]
        public List<Workflow> Workflows()
        {
            return this._workflowRepository.GetWorkflows();
        }

        /// <summary>
        /// Details the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>DetailedWorkflow.</returns>
        [HttpGet]
        public DetailedWorkflow Detail(string id)
        {
            return this._workflowRepository.GetDetailedWorkflow(id);
        }
    }
}