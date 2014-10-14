using System;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using BE.ModelosIII.Infrastructure.ApplicationServices;
using BE.ModelosIII.Mvc.Areas.Api.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace BE.ModelosIII.Mvc.Components.Security
{
    public class RequiresAuthAttribute : System.Web.Http.AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
            else
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = ServiceLocator.Current.GetInstance<IEncryptionService>().DecryptToken(authToken);
                var parameters = decodedToken.Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
                var email = parameters[0];
                actionContext.ControllerContext.RouteData.Values.Add("email", email);
                var expirationDate = DateTime.Parse(parameters[1], CultureInfo.InvariantCulture);
                if (expirationDate < DateTime.Now)
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
                else
                {
                    SetPrincipal(email);

                    var authController = actionContext.ControllerContext.Controller as IAuthController;
                    if (authController != null)
                    {
                        SetPrincipal(email);
                        authController.UserIdentityEmail = email;
                    }
                }
            }
        }

        private void SetPrincipal(string email)
        {
            var principal = new GenericPrincipal(new GenericIdentity(email), new[] {"Usuario"});
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }
    }
}