using System.Diagnostics.Contracts;
using System.Web.Mvc;
using System.Web.Routing;

namespace BE.ModelosIII.Mvc.Components.Security
{
    public class RequiresAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            Contract.Requires(filterContext != null);

            var context = filterContext.RequestContext.HttpContext;

            if (context.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary 
                                   {
                                       { "area", string.Empty},
                                       { "action", "AccessDenied" },
                                       { "controller", "Security" }
                                   });
            }
            else
            {
                string extraQueryString = context.Request.RawUrl;

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary 
                                   {
                                       { "area", string.Empty},
                                       { "action", "LogIn" },
                                       { "controller", "Security" },
                                       { "returnUrl", extraQueryString }
                                   });
            }
        }
    }

}