using System.Web.Mvc;
using BE.ModelosIII.Mvc.Controllers;

namespace BE.ModelosIII.Mvc.Areas.Owner.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
