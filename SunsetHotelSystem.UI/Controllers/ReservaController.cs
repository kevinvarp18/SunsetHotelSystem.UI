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
using System.Net.Mail;
using System.Net;

namespace SunsetHotelSystem.UI.Controllers {
    public class ReservaController : ConfigController {

        public async Task<ActionResult> HabitacionDisponible() {
            List<TSH_Tipo_Habitacion> listaTipoHabitaciones = new List<TSH_Tipo_Habitacion>();
            Respuesta<List<TSH_Tipo_Habitacion>> respuesta = new Respuesta<List<TSH_Tipo_Habitacion>>();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync("api/TSH_Tipo_Habitacion");
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<List<TSH_Tipo_Habitacion>>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    listaTipoHabitaciones = respuesta.valorRetorno;
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            return View("HabitacionDisponible", listaTipoHabitaciones);
        }//Fin de la funcion HabitacionDisponible.

        [HttpPost]
        public async Task<ActionResult> HabitacionDisponible(DateTime fechaLlegada, DateTime fechaSalida, int tipoHabitacion) {
            DateTime fechaActual = DateTime.Now;
            if (fechaLlegada > fechaSalida || fechaLlegada == fechaSalida || fechaLlegada < fechaActual || fechaSalida < fechaActual) {
                return RedirectToAction("HabitacionDisponible", "Reserva");
            } else {
                List<SP_ConsultarDisponibilidad_Result> habitacionesDisponibles = new List<SP_ConsultarDisponibilidad_Result>();
                Respuesta<List<SP_ConsultarDisponibilidad_Result>> respuesta = new Respuesta<List<SP_ConsultarDisponibilidad_Result>>();
                try {
                    HttpResponseMessage responseWAPI = await webAPI.GetAsync(String.Concat("api/TSH_Tipo_Habitacion/", tipoHabitacion));
                    if (responseWAPI.IsSuccessStatusCode) {
                        respuesta = JsonConvert.DeserializeObject<Respuesta<List<SP_ConsultarDisponibilidad_Result>>>(responseWAPI.Content.ReadAsStringAsync().Result);
                        habitacionesDisponibles = respuesta.valorRetorno;
                    }//Fin del if.
                } catch (Exception ex) {
                    System.Console.Write(ex.ToString());
                }//Fin del try-catch.

                if (habitacionesDisponibles.Count > 0)
                    return RedirectToAction("Reserva", "Reserva", new { idHabitacion = habitacionesDisponibles.First().TN_Identificador_TSH_Habitacion, fechaLlegada = fechaLlegada, fechaSalida = fechaSalida });
                else
                    return RedirectToAction("ResultadoReserva", "Reserva", new { nombreCliente = "", correoElectronico = "", numeroReserva = "", resultadoReserva = "2" });
            }//Verifica que las fechas tengan coherencia.
        }//Fin de la función HabitacionDisponible.

        
        public async Task<ActionResult> Reserva(int idHabitacion, DateTime fechaLlegada, DateTime fechaSalida) {
            TSH_Habitacion habitacion = new TSH_Habitacion();
            Respuesta<TSH_Habitacion> respuesta = new Respuesta<TSH_Habitacion>();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync(String.Concat("api/TSH_Habitacion/", idHabitacion));
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Habitacion>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    habitacion = respuesta.valorRetorno;
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            ViewData["fechaLlegada"] = fechaLlegada;
            ViewData["fechaSalida"] = fechaSalida;

            return View(habitacion);
        }//Fin de la función Reserva.

        [HttpPost]
        public async Task<ActionResult> Reserva(string nombreReserva, string apellidoReserva, string correoReserva, string tarjetaReserva, int numeroHabitacion, DateTime fechaLlegada, DateTime fechaSalida) {
            fechaLlegada = fechaLlegada.Date + (new TimeSpan(15, 0, 0));
            fechaSalida = fechaSalida.Date + (new TimeSpan(11, 0, 0));
            Respuesta<TSH_Reserva> respuesta = new Respuesta<TSH_Reserva>();
            TSH_Reserva reserva = new TSH_Reserva();
            reserva.TSH_Cliente = new TSH_Cliente();
            try {
                reserva.TD_Fecha_Ingreso_TSH_Reserva = fechaLlegada;
                reserva.TD_Fecha_Salida_TSH_Reserva = fechaSalida;
                reserva.TN_Num_Habitacion_TSH_Reserva = numeroHabitacion;
                reserva.TSH_Cliente.TC_Nombre_TSH_Cliente = nombreReserva;
                reserva.TSH_Cliente.TC_Apellidos_TSH_Cliente = apellidoReserva;
                reserva.TSH_Cliente.TC_Tarjeta_TSH_Cliente = tarjetaReserva;
                reserva.TSH_Cliente.TC_correo_TSH_Cliente = correoReserva;

                String jsonContent = JsonConvert.SerializeObject(reserva);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                ByteArrayContent byteArrayContent = new ByteArrayContent(buffer);
                byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage responseWAPI = await webAPI.PostAsync("api/TSH_Reserva/", byteArrayContent);

                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Reserva>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    reserva = respuesta.valorRetorno;
                }//Fin del if.
                Guid g = Guid.NewGuid();
                correo(nombreReserva, correoReserva, apellidoReserva, numeroHabitacion,g.ToString());
                return RedirectToAction("ResultadoReserva", "Reserva", new { nombreCliente = nombreReserva + " " + apellidoReserva, correoElectronico = correoReserva, numeroReserva = g.ToString(), resultadoReserva = "1" });
            } catch {
                return RedirectToAction("Index", "Home");
            }//Fin del try-catch.
        }//Fin del método Reserva.

        public ActionResult ResultadoReserva(string nombreCliente, string correoElectronico, string numeroReserva, string resultadoReserva) {
            ViewData["Cliente"] = nombreCliente;
            ViewData["Correo"] = correoElectronico;
            ViewData["NumeroReserva"] = numeroReserva;
            ViewData["Resultado"] = resultadoReserva;
            return View();
        }//Fin de la función ResultadoReserva.

        public void correo(string nombreReserva, string correoReserva, string apellidoReserva, int numeroHabitacion, string cod)
        {
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress(correoReserva));
            email.From = new MailAddress("sunsethotelinfo@gmail.com");
            email.Subject = "Reservación Comprobante( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
            email.Body = "Reciba un cordial saludo " + nombreReserva + " " + apellidoReserva + " por parte de Sunset Hotel. Le adjuntamos el  número de habitación asiganada: " + numeroHabitacion + ". Además, tiene que presentar el siguiente código de comprobante para verificar su reservación " + cod;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 25;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("sunsethotelinfo@gmail.com", "sunsethotel1123");

            try
            {
                smtp.Send(email);

            }
            catch (Exception except)
            {
                email.Dispose();
            }
        }//fin correo


        [HttpPost]
        public JsonResult consulta( string tarjeta)
        {
            return Json("");
        }
    }//Fin de la clase ReservaController.
}//Fin del namespace.