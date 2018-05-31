using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunsetHotelSystem.UI.Controllers {
    public class AdministradorController : Controller {
        public ActionResult Home() {
            Session["Usuario"] = "1";
            return View();
        }//Fin del método Home.
    }//Fin de la clase AdminController.
}//Fin del namespace.