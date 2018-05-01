using System.Web;
using System.Web.Optimization;

namespace SunsetHotelSystem.UI {
    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.4.min.js",
                        "~/Scripts/main.js",
                        "~/Scripts/lightbox-plus-jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
                        "~/Scripts/move-top.js",
                        "~/Scripts/easing.js",
                        "~/Scripts/jquery.typer.js",
                        "~/Scripts/easy-responsive-tabs.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/popper.js",
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/font-awesome.css",
                      "~/Content/easy-responsive-tabs.css",
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/style.css"));
        }
    }//Fin de la clase BundleConfig.
}//Fin del namespace.
