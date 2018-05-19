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
            TSH_Pagina pagina = new TSH_Pagina();
            Respuesta<TSH_Pagina> respuesta = new Respuesta<TSH_Pagina>();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync(String.Concat("api/TSH_Pagina/", 5));
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Pagina>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    pagina = respuesta.valorRetorno;
                }//Fin del if.
            }
            catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.
            return View(pagina);
        }//Fin del método Index.

        public async Task<ActionResult> SobreNosotros() {
            TSH_Pagina pagina = new TSH_Pagina();
            Respuesta<TSH_Pagina> respuesta = new Respuesta<TSH_Pagina>();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync(String.Concat("api/TSH_Pagina/", 6));
                if (responseWAPI.IsSuccessStatusCode)
                {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Pagina>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    pagina = respuesta.valorRetorno;
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.
            return View(pagina);
        }//Fin del método SobreNosotros.

        public async Task<ActionResult> ComoLlegar() {
            TSH_Pagina pagina = new TSH_Pagina();
            Respuesta<TSH_Pagina> respuesta = new Respuesta<TSH_Pagina>();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync(String.Concat("api/TSH_Pagina/", 8));
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Pagina>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    pagina = respuesta.valorRetorno;
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.
            return View(pagina);
        }//Fin del método ComoLlegar.

        public async Task<ActionResult> Facilidades() {
            List<TSH_Pag_Facilidades> listaFacilidades = new List<TSH_Pag_Facilidades>();
            Respuesta<List<TSH_Pag_Facilidades>> respuesta = new Respuesta<List<TSH_Pag_Facilidades>>();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync("api/TSH_Pag_Facilidades");
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<List<TSH_Pag_Facilidades>>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    listaFacilidades = respuesta.valorRetorno;
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            return View(listaFacilidades);
        }//Fin del método Facilidades.

        public ActionResult Contactenos() {
            return View();
        }//Fin del método Contactenos.

        public ActionResult Tarifas() {
            return View();
        }//Fin del método Tarifas.
    }//Fin de la clase HomeController.
}//Fin del namespace.