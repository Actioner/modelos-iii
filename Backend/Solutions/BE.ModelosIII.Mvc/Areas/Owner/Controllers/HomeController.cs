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


        public ActionResult Soon()
        {
            return View();
        }

        public ActionResult PleaseWait()
        {
            return PartialView("_PleaseWait");
        }

        public ActionResult ErrorProcessing()
        {
            return PartialView("_ErrorProcessing");
        }
    }
}
