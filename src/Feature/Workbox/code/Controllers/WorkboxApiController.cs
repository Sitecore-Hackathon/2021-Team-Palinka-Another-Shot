namespace Feature.Workbox.Controllers
{
    using Feature.Workbox.Interfaces;
    using Feature.Workbox.Models;
    using Feature.Workbox.Services;
    using Sitecore.Services.Infrastructure.Web.Http;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;

    //[Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WorkboxApiController : ServicesApiController
    {
        private readonly IWorkflowRepository _workflowRepository;
       public WorkboxApiController()
        {
            this._workflowRepository = new WorkflowRepository();
        }

        [HttpGet]
        public string Ok()
        {
            return "Ok";
        }

        [HttpGet]
        public List<Workflow> Workflows()
        {
            return this._workflowRepository.GetWorkflows();
        }

        [HttpGet]
        public DetailedWorkflow Detail(string id)
        {
            return this._workflowRepository.GetDetailedWorkflow(id);
        }
    }
}