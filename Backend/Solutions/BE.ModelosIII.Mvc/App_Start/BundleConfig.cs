using System.Web.Optimization;

namespace BE.ModelosIII.Mvc
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Ignore("*.unobtrusive-ajax.min.js", OptimizationMode.WhenDisabled);


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/js/jquery.js",
                        "~/Content/js/jquery.cookie.js",
                        "~/Content/js/jquery.datatable.js",
                        "~/Content/js/moment/moment.js",
                        "~/Content/js/globalize/globalize.js",
                        "~/Content/js/globalize/cultures/globalize.culture.es-AR.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Content/js/jquery.validate.js",
                       "~/Content/js/jquery.validate.unobtrusive.js",
                       "~/Content/js/jquery.unobtrusive-ajax.js"));

            //Layout Bundle
            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                "~/Content/js/utopia.js",
                "~/Content/js/common.js",
                "~/Content/js/Layout/layout.js",
                "~/Content/js/select2.js",
                "~/Content/js/alerts.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/mainlayout").Include(
                "~/Content/js/header.js",
                "~/Content/js/sidebar.js"
                ));
            
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/js/modernizr-*"));

            bundles.Add(new StyleBundle("~/layout/css").Include(
                 "~/Content/styles/utopia-white.css",
                "~/Content/styles/utopia-responsive.css",
                "~/Content/styles/alerts.css",
                "~/Content/styles/custom.css"));

            bundles.Add(new StyleBundle("~/mainlayout/css").Include(
                "~/Content/styles/ie.css",
                "~/Content/styles/queryLoader.css"));




        }
    }
}