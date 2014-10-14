using System.Linq;
using System.Web.Mvc;

namespace BE.ModelosIII.Mvc.Components.ViewEngine
{
    public class BackEndViewEngine : RazorViewEngine
    {
        private static readonly string[] NewPartialViewFormats = new[] { 
                "~/Views/{1}/Account/{0}.cshtml",
                "~/Views/Shared/Account/{0}.cshtml",
                "~/Views/{1}/Util/{0}.cshtml",
                "~/Views/Shared/Util/{0}.cshtml",
            };

        public BackEndViewEngine()
        {
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(NewPartialViewFormats).ToArray();
        }
    }
}
