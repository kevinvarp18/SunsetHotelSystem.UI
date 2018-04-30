using System.Web;
using System.Web.Optimization;

namespace SunsetHotelSystem.UI {
    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.4.min.js",
                        "~/Scripts/main.js",
                        "~/Scripts/move-top.js",
                        "~/Script/easing.js",
                        "~/Script/jquery.typer.js",
                        "~/Script/easy-reponsive-tabs.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/font-awesome.css",
                      "~/Content/easy-responsive-tabs.css",
                      "~/Content/bootstrap.css",
                      "~/Content/style.css"));
        }
    }//Fin de la clase BundleConfig.
}//Fin del namespace.
