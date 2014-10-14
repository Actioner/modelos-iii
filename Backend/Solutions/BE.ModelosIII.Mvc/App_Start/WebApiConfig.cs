using System.Web.Http;
using BE.ModelosIII.Mvc.Components.Filter;

namespace BE.ModelosIII.Mvc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new ValidationActionFilter());
        }
    }
}
