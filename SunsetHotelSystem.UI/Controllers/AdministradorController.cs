using SunsetHotelSystem.Dominio.Entidades;
using SunsetHotelSystem.Dominio.UTL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SunsetHotelSystem.UI.Controllers {
    public class AdministradorController : ConfigController {
        public ActionResult Home() {
            Session["Usuario"] = "1";
            return View();
        }//Fin del método Home.

        public ActionResult Login() {
            if (Session["Usuario"].Equals("0")) 
                return View();
            else
                return RedirectToAction("Home");
        }//Fin del método Login.

        public Task<ActionResult> AutenticarUsuario(string usuario, string contrasena) {

        }//Fin del método AutenticarUsuario.
    }//Fin de la clase AdminController.
}//Fin del namespace.