using System.Web;
using System.Web.Optimization;

namespace SunsetHotelSystem.UI {
    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/main.js",
                        "~/Scripts/jquery-2.1.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/move-top.js",
                      "~/Scripts/easing.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/jquery.typer.js",
                      "~/Scripts/easy-responsive-tabs.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/font-awesome.css",
                      "~/Content/easy-responsive-tabs.css",
                      "~/Content/bootstrap.css",
                      "~/Content/style.css"));
        }
    }//Fin de la clase BundleConfig.
}//Fin del namespace.
