using System.Web.Mvc;

namespace BE.ModelosIII.Mvc.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            ViewBag.Title = "No encontrado - ModelosIII Panel";
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}
