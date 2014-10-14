using System.Web.Mvc;
using System.Web.Routing;

namespace BE.ModelosIII.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "NotFound",
              url: "NotFound/{id}",
              defaults: new { controller = "Error", action = "NotFound", id = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Security", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}