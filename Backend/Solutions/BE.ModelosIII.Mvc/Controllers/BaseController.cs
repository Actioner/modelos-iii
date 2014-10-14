using System.IO;
using System.Web.Mvc;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Mvc.Components.Security;
using Microsoft.Practices.ServiceLocation;

namespace BE.ModelosIII.Mvc.Controllers
{
    [Requires(Roles = "Administrador")]
    public class BaseController : Controller
    {
        private readonly IUserRepository _userRepository;

        public BaseController()
        {
            _userRepository = ServiceLocator.Current.GetInstance<IUserRepository>();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionDescriptor = filterContext.ActionDescriptor;
            
            ViewBag.Layout = "~/Areas/Owner/Views/Shared/_OwnerLayout.cshtml";
            ViewBag.ActionName = actionDescriptor.ActionName;
            ViewBag.ControllerName = actionDescriptor.ControllerDescriptor.ControllerName;
        }
    
        protected string RenderPartialViewToString(string viewName, object model = null, string prefix = null)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            }

            if (model != null)
            {
                ViewData.Model = model;
            }

            if (prefix != null)
            {
                ViewData.TemplateInfo.HtmlFieldPrefix = prefix;
            }

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.ToString();
            }
        }

        protected User GetCurrentUser()
        {
            return _userRepository.GetByEmail(HttpContext.User.Identity.Name);
        }
    }
}
