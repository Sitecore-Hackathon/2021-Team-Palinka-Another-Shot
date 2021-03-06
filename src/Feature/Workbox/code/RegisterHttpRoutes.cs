namespace Feature.Workbox
{
    using Sitecore.Pipelines;
    using System.Web.Http;
    using System.Web.Routing;

    public class RegisterHttpRoutes
    {
        public virtual void Process(PipelineArgs args)
        {
            RegisterRoute(RouteTable.Routes);
        }

        /// <summary>
        /// Registers the route.
        /// </summary>
        /// <remarks>The order of the routes is important.</remarks>
        /// <param name="routes">The routes.</param>
        protected virtual void RegisterRoute(RouteCollection routes)
        {

            RouteTable.Routes.MapHttpRoute("WorkboxApi",
                "sitecore/api/ssc/workbox/{action}",
                new { controller = "WorkboxApi" });

            RouteTable.Routes.MapHttpRoute("WorkboxApiDetail",
                "sitecore/api/ssc/workbox/{action}/{id}",
                new { controller = "WorkboxApi" });
        }
    }
}