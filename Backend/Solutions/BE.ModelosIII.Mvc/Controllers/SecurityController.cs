using System.Web.Mvc;
using System.Web.Security;
using BE.ModelosIII.Tasks.Commands.Security;
using SharpArch.Domain.Commands;
using SharpArch.NHibernate.Web.Mvc;

namespace BE.ModelosIII.Mvc.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        private readonly ICommandProcessor _commandProcessor;

        public SecurityController(ICommandProcessor commandProcessor)
        {
            this._commandProcessor = commandProcessor;
        }

        public ActionResult LogIn()
        {
            return View(new LoginCommand());
        }

        [HttpPost]
        public ActionResult LogIn(LoginCommand command, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(command.Email, command.RememberMe);

                return Redirect(returnUrl ?? Url.Action("Home", "Security"));
            }
            return View(command);
        }
        
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult Home()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Owner" });
            }

            return RedirectToAction("Login");
        }
    }
}
