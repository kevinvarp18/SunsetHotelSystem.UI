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
    public class PruebaController : ConfigController {

        [HttpPost]
        public async Task<ActionResult> InsertarHabitacion(FormCollection collection) {
            Respuesta<TSH_Habitacion> respuesta = new Respuesta<TSH_Habitacion>();
            TSH_Habitacion habitacion = new TSH_Habitacion();
            try {
                //user.name = collection.GetValue("name").AttemptedValue.ToString();
                //user.lastName = collection.GetValue("lastName").AttemptedValue.ToString();
                //user.identification = collection.GetValue("identification").AttemptedValue.ToString();
                //user.email = collection.GetValue("email").AttemptedValue.ToString();
                String jsonContent = JsonConvert.SerializeObject(habitacion);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                ByteArrayContent byteArrayContent = new ByteArrayContent(buffer);
                byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage responseAPI = await webAPI.PostAsync("api/user/", byteArrayContent);
                if (responseAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Habitacion>>(responseAPI.Content.ReadAsStringAsync().Result);
                    habitacion = respuesta.valorRetorno;
                }//Fin del if.
                return RedirectToAction("Index");
            } catch {
                return View();
            }//Try-Catch.
        }//Fin del método InsertarHabitacion.

        public async Task<ActionResult> GetTipoHabitacion() {
            List<TSH_Tipo_Habitacion> listaHabitaciones = new List<TSH_Tipo_Habitacion>();
            Respuesta<List<TSH_Tipo_Habitacion>> respuesta = new Respuesta<List<TSH_Tipo_Habitacion>>();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync("api/Prueba");
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<List<TSH_Tipo_Habitacion>>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    listaHabitaciones = respuesta.valorRetorno;
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            return View("ListaHabitaciones", listaHabitaciones);
        }//Fin del método ListaHabitaciones.

    }//Fin de la clase HabitacionController
}//Fin del namespace.