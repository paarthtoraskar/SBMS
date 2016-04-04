using System.Web.Mvc;
using System.Web.Routing;

namespace SBMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new { controller = "Product", action = "Catalog", id = UrlParameter.Optional }
                );
        }
    }
}