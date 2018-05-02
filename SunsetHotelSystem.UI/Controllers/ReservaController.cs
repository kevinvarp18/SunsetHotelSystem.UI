using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunsetHotelSystem.UI.Controllers {
    public class ReservaController : ConfigController {

        public ActionResult HabitacionDisponible() {
            return View();
        }
    }
}