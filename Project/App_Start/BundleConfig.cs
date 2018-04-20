using System.Web;
using System.Web.Optimization;

namespace HandsOn
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery-common-1.0.0.js",
                      "~/Scripts/chosen.jquery.js",
                      "~/Scripts/bootstrap-notify.js",
                      "~/Scripts/toastr.min.js",
                      "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/loaded").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/chosen.jquery.js",
                      "~/Scripts/bootstrap-notify.js",
                      "~/Scripts/toastr.min.js",
                      "~/Scripts/jquery-loaded-1.0.0.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-theme.min.css",
                       "~/Content/chosen.css",
                       "~/Content/toastr.css",
                       "~/Content/bootstrap-notify.css",
                      "~/Content/site.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
