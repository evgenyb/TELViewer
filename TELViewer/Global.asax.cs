using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TELViewer.Infrastructure;

namespace TELViewer
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Log", action = "List", id = "" }  // Parameter defaults
            );
            
            routes.MapRoute(
                "DefaultWithASPX",                                              // Route name
                "{controller}.aspx/{action}/{id}",                           // URL with parameters
                new { controller = "Log", action = "List", id = "" }  // Parameter defaults
            );

            RegisterIoCController();
        }

        private static void RegisterIoCController()
        {
            Bootstrapper.CreateContainer();
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}