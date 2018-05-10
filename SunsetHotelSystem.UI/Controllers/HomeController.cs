using SunsetHotelSystem.Dominio.Entidades.Entidades;
using SunsetHotelSystem.Dominio.UTL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SunsetHotelSystem.UI.Controllers {
    public class HomeController : ConfigController {

        public async Task<ActionResult> Index() {
            if ((Session["Usuario"] == null)) {
                Session["Usuario"] = "0";
            }

            TSH_Pag_Home paginaHome = new TSH_Pag_Home();
            Respuesta<TSH_Pag_Home> respuesta = new Respuesta<TSH_Pag_Home>();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync("api/TSH_Pag_Home");
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Pag_Home>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    paginaHome = respuesta.valorRetorno;
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            return View(paginaHome);
        }//Fin del método Index.

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