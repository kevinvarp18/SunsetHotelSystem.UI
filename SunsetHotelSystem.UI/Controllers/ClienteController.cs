using SunsetHotelSystem.Dominio.Entidades;
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
using SunsetHotelSystem.UI.Models;

namespace SunsetHotelSystem.UI.Controllers {
    public class ClienteController : ConfigController {

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

        public async Task<ActionResult> Tarifas() {
            List<TSH_Tipo_Habitacion> listaTiposHabitacion = new List<TSH_Tipo_Habitacion>();
            List<TSH_Caracteristica_habitacion> listaCaracteristicasHabitaciones = new List<TSH_Caracteristica_habitacion>();
            Respuesta<List<TSH_Tipo_Habitacion>> respuestaTipoHabitacion = new Respuesta<List<TSH_Tipo_Habitacion>>();
            Respuesta<List<TSH_Caracteristica_habitacion>> respuestaCaracteristicasHabitacion = new Respuesta<List<TSH_Caracteristica_habitacion>>();

            try {
                HttpResponseMessage responseTipoHabitacionWAPI = await webAPI.GetAsync("api/TSH_Tipo_Habitacion");
                if (responseTipoHabitacionWAPI.IsSuccessStatusCode){
                    respuestaTipoHabitacion = JsonConvert.DeserializeObject<Respuesta<List<TSH_Tipo_Habitacion>>>(responseTipoHabitacionWAPI.Content.ReadAsStringAsync().Result);
                    listaTiposHabitacion = respuestaTipoHabitacion.valorRetorno;
                }//Fin del if.

                HttpResponseMessage responseCaracteristicaHabitacionWAPI = await webAPI.GetAsync("api/TSH_Caracteristica_Habitacion");
                if (responseCaracteristicaHabitacionWAPI.IsSuccessStatusCode) {
                    respuestaCaracteristicasHabitacion = JsonConvert.DeserializeObject<Respuesta<List<TSH_Caracteristica_habitacion>>>(responseCaracteristicaHabitacionWAPI.Content.ReadAsStringAsync().Result);
                    listaCaracteristicasHabitaciones = respuestaCaracteristicasHabitacion.valorRetorno;
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            Tarifa tarifas = new Tarifa(listaCaracteristicasHabitaciones, listaTiposHabitacion);

            return View(tarifas);
        }//Fin del método Tarifas.
    }//Fin de la clase ClienteController.
}//Fin del namespace.