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
            Session["Usuario"] = "0";

            TSH_Pagina paginaHome = new TSH_Pagina();
            Respuesta<TSH_Pagina> respuesta = new Respuesta<TSH_Pagina>();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync(String.Concat("api/TSH_Pagina/", 5));
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Pagina>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    paginaHome = respuesta.valorRetorno;
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            return View(paginaHome);
        }//Fin del método Index.

        public async Task<ActionResult> SobreNosotros() {
            TSH_Pagina paginaSobreNosotros = new TSH_Pagina();
            Respuesta<TSH_Pagina> respuesta = new Respuesta<TSH_Pagina>();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync(String.Concat("api/TSH_Pagina/", 6));
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Pagina>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    paginaSobreNosotros = respuesta.valorRetorno;
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            return View(paginaSobreNosotros);
        }//Fin del método SobreNosotros.

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