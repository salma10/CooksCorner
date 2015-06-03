using System.Web;
using System.Web.Optimization;

namespace CooksCorner
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            // all css needed for home layout designing
            bundles.Add(new StyleBundle("~/HomeLayoutCss").Include(
                "~/Content/home/css/reset.css",
                "~/Content/home/css/ie.css",
                "~/Content/home/css/layout.css",
                "~/Content/home/css/style.css"));

            // all js requird for home layout operating bgSlider.js

            bundles.Add(new ScriptBundle("~/HomeLayoutScript").Include(
                        "~/Content/home/js/bgSlider.js",
                        "~/Content/home/js/cufon-replace.js",
                        "~/Content/home/js/cufon-yui.js",
                        "~/Content/home/js/easyTooltip.js",
                        "~/Content/home/js/FF-cash.js",
                        "~/Content/home/js/html5.js",
                        "~/Content/home/js/jquery-1.6.3.min.js",
                        "~/Content/home/js/jquery.easing.1.3.js",
                        "~/Content/home/js/Lobster_13_400.font.js",
                        "~/Content/home/js/NewsGoth_BT_400.font.js",
                        "~/Content/home/js/PIE.htc",
                        "~/Content/home/js/script.js",
                        "~/Content/home/js/tms-0.3.js",
                        "~/Content/home/js/tms_presets.js"));
                        

        }
    }
}