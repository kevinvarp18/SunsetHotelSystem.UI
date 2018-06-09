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

        public async Task<ActionResult> AutenticarUsuario(string usuario, string contrasena) {
            TSH_Administrador administrador = new TSH_Administrador();
            Respuesta<TSH_Administrador> respuesta = new Respuesta<TSH_Administrador>();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync(String.Concat("api/TSH_Administrador/", administrador));
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Administrador>>(responseWAPI.Content.ReadAsStringAsync().Result);
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            if (respuesta.resultado == 1)
                return RedirectToAction("Home");
            else
                return RedirectToAction("Login");
        }//Fin del método AutenticarUsuario.
    }//Fin de la clase AdminController.
}//Fin del namespace.