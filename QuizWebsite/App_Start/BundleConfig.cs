using System.Web;
using System.Web.Optimization;

namespace QuizWebsite
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.0.js",
            "~/Scripts/bootstrap.js",
            "~/Scripts/common.js",
            "~/Scripts/Datatables/jquery.dataTables.js",
            "~/Scripts/Datatables/dataTables.bootstrap.js",
            "~/Scripts/Toster.js",
            "~/Scripts/jquery.blockUI.js"

            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/DataTables/jquery.dataTables.css",
                        "~/Content/DataTables/dataTables.bootstrap.css",
                        "~/Content/Toster.css",
                        "~/Content/site.css",
                        "~/Content/ThemeCss/layout.css",
                        "~/Content/ThemeCss/components-md.css",
                        "~/Content/ThemeCss/plugins-md.css",
                        "~/Content/ThemeCss/custom.css",
                        "~/Content/ThemeCss/blue.css",
                        "~/Content/ThemeCss/font-awesome.css",
                        "~/Content/ThemeCss/simple-line-icons.css"
                        ));
        }
    }
}