namespace Feature.Workbox.Controllers
{
    using Feature.Workbox.Models;
    using Sitecore.Services.Infrastructure.Web.Http;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;

    //[Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WorkboxApiController : ServicesApiController
    {
       public WorkboxApiController()
        {

        }

        [HttpGet]
        public string Ok()
        {
            return "Ok";
        }

        [HttpGet]
        public Workbox Workflows()
        {
            return new Workbox();
        }
    }
}