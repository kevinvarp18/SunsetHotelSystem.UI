using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunsetHotelSystem.UI.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            if ((Session["Usuario"] == null)) {
                Session["Usuario"] = "0";
            }
                return View();
        }

        public ActionResult SobreNosotros() {
            return View();
        }

        public ActionResult ComoLlegar() { 
            return View();
        }

        public ActionResult Facilidades() {
            return View();
        }

        public ActionResult Contactenos() {
            return View();
        }

        public ActionResult Tarifas() {
            return View();
        }
    }//Fin de la clase HomeController.
}//Fin del namespace.