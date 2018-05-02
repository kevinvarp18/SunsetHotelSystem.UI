using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunsetHotelSystem.UI.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult SobreNosotros() {
            return View();
        }

        public ActionResult ComoLlegar() { 
            return View();
        }

        public ActionResult facilidades() {
            return View();
        }
    }//Fin de la clase HomeController.
}//Fin del namespace.