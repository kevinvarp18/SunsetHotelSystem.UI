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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SunsetHotelSystem.UI.Controllers {
    public class HabitacionesController : ConfigController {
        public async Task<ActionResult> ListaHabitaciones() {
            List<TSH_Tipo_Habitacion> listaTiposHabitacion = new List<TSH_Tipo_Habitacion>();
            List<TSH_Habitacion> listaHabitaciones = new List<TSH_Habitacion>();
            Respuesta<List<TSH_Tipo_Habitacion>> respuestaTipoHabitacion = new Respuesta<List<TSH_Tipo_Habitacion>>();
            Respuesta<List<TSH_Habitacion>> respuestaHabitaciones = new Respuesta<List<TSH_Habitacion>>();

            try {
                HttpResponseMessage responseTipoHabitacionWAPI = await webAPI.GetAsync("api/TSH_Tipo_Habitacion");
                if (responseTipoHabitacionWAPI.IsSuccessStatusCode) {
                    respuestaTipoHabitacion = JsonConvert.DeserializeObject<Respuesta<List<TSH_Tipo_Habitacion>>>(responseTipoHabitacionWAPI.Content.ReadAsStringAsync().Result);
                    listaTiposHabitacion = respuestaTipoHabitacion.valorRetorno;
                }//Fin del if.

                HttpResponseMessage responseHabitacionWAPI = await webAPI.GetAsync("api/TSH_Habitacion");
                if (responseHabitacionWAPI.IsSuccessStatusCode) {
                    respuestaHabitaciones = JsonConvert.DeserializeObject<Respuesta<List<TSH_Habitacion>>>(responseHabitacionWAPI.Content.ReadAsStringAsync().Result);
                    listaHabitaciones = respuestaHabitaciones.valorRetorno;
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            Habitacion habitaciones = new Habitacion(listaTiposHabitacion, listaHabitaciones);

            return View(habitaciones);
        }//Fin del método ListaHabitaciones.

        public async Task<ActionResult> disponibilidadDiaHoy()
        {
            DateTime fechaActual = DateTime.Now;
            List<TSH_Tipo_Habitacion> listaTiposHabitacion = new List<TSH_Tipo_Habitacion>();
            List<TSH_Habitacion> listaHabitaciones = new List<TSH_Habitacion>();
            List<TSH_Habitacion> listaHabitacionesReservadas = new List<TSH_Habitacion>();
            Respuesta<List<TSH_Tipo_Habitacion>> respuestaTipoHabitacion = new Respuesta<List<TSH_Tipo_Habitacion>>();
            Respuesta<List<TSH_Habitacion>> respuestaHabitaciones = new Respuesta<List<TSH_Habitacion>>();

            //agregado de reservas

            List<TSH_Reserva> listaReservas = new List<TSH_Reserva>();
            Respuesta<List<TSH_Reserva>> respuesta = new Respuesta<List<TSH_Reserva>>();

            try
            {
                HttpResponseMessage responseTipoHabitacionWAPI = await webAPI.GetAsync("api/TSH_Tipo_Habitacion");
                if (responseTipoHabitacionWAPI.IsSuccessStatusCode)
                {
                    respuestaTipoHabitacion = JsonConvert.DeserializeObject<Respuesta<List<TSH_Tipo_Habitacion>>>(responseTipoHabitacionWAPI.Content.ReadAsStringAsync().Result);
                    listaTiposHabitacion = respuestaTipoHabitacion.valorRetorno;
                }//Fin del if.

                HttpResponseMessage responseHabitacionWAPI = await webAPI.GetAsync("api/TSH_Habitacion");
                if (responseHabitacionWAPI.IsSuccessStatusCode)
                {
                    respuestaHabitaciones = JsonConvert.DeserializeObject<Respuesta<List<TSH_Habitacion>>>(responseHabitacionWAPI.Content.ReadAsStringAsync().Result);
                    listaHabitaciones = respuestaHabitaciones.valorRetorno;
                }//Fin del if.
                HttpResponseMessage responseWAPI = await webAPI.GetAsync("api/TSH_Reserva/");
                if (responseWAPI.IsSuccessStatusCode)
                {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<List<TSH_Reserva>>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    listaReservas = respuesta.valorRetorno;
                }//Fin del if.
                
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.
            foreach (TSH_Reserva reserva in listaReservas)
            {
                if (reserva.TD_Fecha_Ingreso_TSH_Reserva == fechaActual)
                {
                    listaHabitacionesReservadas.Add(reserva.TSH_Habitacion);
                }//Fin del if.
            }//Fin del foreach.

            Habitacion habitaciones = new Habitacion(listaTiposHabitacion, listaHabitaciones,listaHabitacionesReservadas);

            return View(habitaciones);
        }//Fin del método ListaHabitaciones.

        public async Task<ActionResult> ModificarHabitacion(int idTipoHabitacion) {
            List<TSH_Tipo_Habitacion> listaTiposHabitacion = new List<TSH_Tipo_Habitacion>();
            Respuesta<List<TSH_Tipo_Habitacion>> respuestaTipoHabitacion = new Respuesta<List<TSH_Tipo_Habitacion>>();
            TSH_Tipo_Habitacion tipoHabitacion = new TSH_Tipo_Habitacion();

            try {
                HttpResponseMessage responseTipoHabitacionWAPI = await webAPI.GetAsync("api/TSH_Tipo_Habitacion");
                if (responseTipoHabitacionWAPI.IsSuccessStatusCode) {
                    respuestaTipoHabitacion = JsonConvert.DeserializeObject<Respuesta<List<TSH_Tipo_Habitacion>>>(responseTipoHabitacionWAPI.Content.ReadAsStringAsync().Result);
                    listaTiposHabitacion = respuestaTipoHabitacion.valorRetorno;
                }//Fin del if.

                foreach (TSH_Tipo_Habitacion tipoHabitacionTemp in listaTiposHabitacion) {
                    if (tipoHabitacionTemp.TN_Identificador_TSH_Tipo_Habitacion == idTipoHabitacion) {
                        tipoHabitacion = tipoHabitacionTemp;
                    }//Fin del if.
                }//Fin del foreach.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            return View(tipoHabitacion);
        }//Fin del método actualizarPaginaHabitacion.

        [HttpPost]
        public async Task<ActionResult> actualizarHabitacion(int id, string descripcion, string tituloHabitacion, float tarifa, HttpPostedFileBase imagen) {
            Respuesta<TSH_Tipo_Habitacion> respuesta = new Respuesta<TSH_Tipo_Habitacion>();
            TSH_Tipo_Habitacion tipoHabitacion = new TSH_Tipo_Habitacion();

            try {
                if (imagen.ContentLength > 0) {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(imagen.InputStream)) {
                        imageData = binaryReader.ReadBytes(imagen.ContentLength);
                    }
                    tipoHabitacion.TI_Imagen_TSH_Tipo_Habitacion = imageData;
                }//Fin del if.
            } catch {
            }//Fin del try-catch.

            Guid g = Guid.NewGuid();
            tipoHabitacion.TN_Identificador_TSH_Tipo_Habitacion = id;
            tipoHabitacion.TN_Id_Imagen_TSH_Tipo_Habitacion = g;
            tipoHabitacion.TC_Titulo_TSH_Tipo_Habitacion = tituloHabitacion;
            tipoHabitacion.TC_Descripcion_TSH_Tipo_Habitacion = descripcion;
            tipoHabitacion.TN_Tarifa_TSH_Tipo_Habitacion = tarifa;

            try {
                String jsonContent = JsonConvert.SerializeObject(tipoHabitacion);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                ByteArrayContent byteArrayContent = new ByteArrayContent(buffer);
                byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage responseWAPI = await webAPI.PutAsync(String.Concat("api/TSH_Tipo_Habitacion"), byteArrayContent);
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Tipo_Habitacion>>(responseWAPI.Content.ReadAsStringAsync().Result);
                }//Fin del if.

                if (respuesta.resultado == 1)
                    ViewBag.Message = "Los cambios en la habitación se realizaron exitosamente.";
                else
                    ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";

                return View("../Administrador/Home");
            } catch {
                ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";
                return View("../Administrador/Home");
            }//Try-catch.
        }//Fin del método actualizarPaginaHabitacion.

        public void generarPDF(){
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\prueba.pdf", FileMode.Create));

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("Mi primer PDF");
            doc.AddCreator("Roberto Torres");

            // Abrimos el archivo
            doc.Open();
        }//End generarPDF

    }//Fin de la clase HabitacionesController.
}//Fin de la namespace.