using Castle.MicroKernel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BE.ModelosIII.Mvc.Components.Windsor
{
    public class WindsorActionInvoker : ControllerActionInvoker
    {
        private readonly IKernel _kernel;

        public WindsorActionInvoker(IKernel kernel)
        {
            this._kernel = kernel;
        }

        protected override ActionExecutedContext InvokeActionMethodWithFilters(
            ControllerContext controllerContext
            , IList<System.Web.Mvc.IActionFilter> actionFilters
            , ActionDescriptor actionDescriptor
            , IDictionary<string, object> parameters)
        {
            foreach (var actionFilter in actionFilters)
            {
                _kernel.InjectProperties(actionFilter);
            }
            return base.InvokeActionMethodWithFilters(controllerContext, actionFilters, actionDescriptor, parameters);
        }

        protected override AuthorizationContext InvokeAuthorizationFilters(ControllerContext controllerContext,
                                                                           IList<IAuthorizationFilter> filters,
                                                                           ActionDescriptor actionDescriptor)
        {
            foreach (IAuthorizationFilter authorizeFilter in filters)
            {
                this._kernel.InjectProperties(authorizeFilter);
            }
            return base.InvokeAuthorizationFilters(controllerContext, filters, actionDescriptor);
        }
    }
}