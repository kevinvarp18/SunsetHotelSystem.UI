﻿using SunsetHotelSystem.Dominio.Entidades;
using SunsetHotelSystem.Dominio.UTL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

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
            if (fechaLlegada > fechaSalida || fechaLlegada < fechaActual || fechaSalida < fechaActual) {
                return RedirectToAction("ResultadoReserva", "Reserva", new { nombreCliente = "", correoElectronico = "", numeroReserva = "", resultadoReserva = "2" });
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
                    return RedirectToAction("Reserva", "Reserva", new { numeroHabitacion = habitacionesDisponibles.First().TN_Numero_Habitacion_TSH_Habitacion, fechaLlegada = fechaLlegada, fechaSalida = fechaSalida});
                else
                    return RedirectToAction("ResultadoReserva", "Reserva", new { nombreCliente = "", correoElectronico = "", numeroReserva = "", resultadoReserva = "2" });
            }//Verifica que las fechas tengan coherencia.
        }//Fin de la función HabitacionDisponible.

        public async Task<ActionResult> Reserva(int numeroHabitacion, DateTime fechaLlegada, DateTime fechaSalida) {
            TSH_Habitacion habitacion = new TSH_Habitacion();
            Respuesta<TSH_Habitacion> respuesta = new Respuesta<TSH_Habitacion>();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync(String.Concat("api/TSH_Habitacion/", numeroHabitacion));
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
        public async Task<ActionResult> Reserva(string cedulaReserva, string nombreReserva, string apellidoReserva, string correoReserva, string tarjetaReserva, int numeroHabitacion, DateTime fechaLlegada, DateTime fechaSalida) {
            fechaLlegada = fechaLlegada.Date + (new TimeSpan(15, 0, 0));
            fechaSalida = fechaSalida.Date + (new TimeSpan(11, 0, 0));
            Respuesta<TSH_Reserva> respuesta = new Respuesta<TSH_Reserva>();
            Respuesta<TSH_Habitacion> respuestaHab = new Respuesta<TSH_Habitacion>();
            TSH_Reserva reserva = new TSH_Reserva();
            reserva.TSH_Cliente = new TSH_Cliente();
            TSH_Habitacion habitacion = new TSH_Habitacion();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync(String.Concat("api/TSH_Habitacion/", numeroHabitacion));
                if (responseWAPI.IsSuccessStatusCode) {
                    respuestaHab = JsonConvert.DeserializeObject<Respuesta<TSH_Habitacion>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    habitacion = respuestaHab.valorRetorno;
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            try {
                Guid g = Guid.NewGuid();
                reserva.TN_Numero_Reserva_TSH_Reserva = g;
                reserva.TD_Fecha_Ingreso_TSH_Reserva = fechaLlegada;
                reserva.TD_Fecha_Salida_TSH_Reserva = fechaSalida;
                reserva.TN_Num_Habitacion_TSH_Reserva = numeroHabitacion;
                reserva.TC_Id_Cliente_TSH_Reserva = cedulaReserva;
                reserva.TSH_Cliente.TC_Cedula_TSH_Cliente = cedulaReserva;
                reserva.TSH_Cliente.TC_Nombre_TSH_Cliente = nombreReserva;
                reserva.TSH_Cliente.TC_Apellidos_TSH_Cliente = apellidoReserva;
                reserva.TSH_Cliente.TC_Tarjeta_TSH_Cliente = tarjetaReserva;
                reserva.TSH_Cliente.TC_Correo_TSH_Cliente = correoReserva;
                double precio = habitacion.TSH_Tipo_Habitacion.TN_Tarifa_TSH_Tipo_Habitacion;
                int tipoHabitacion = habitacion.TN_Id_TipoH_TSH_Habitacion;
                precio = promocion(fechaLlegada, fechaSalida, tipoHabitacion, precio);           

                String jsonContent = JsonConvert.SerializeObject(reserva);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                ByteArrayContent byteArrayContent = new ByteArrayContent(buffer);
                byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage responseWAPI = await webAPI.PostAsync("api/TSH_Reserva/", byteArrayContent);

                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Reserva>>(responseWAPI.Content.ReadAsStringAsync().Result);
                }//Fin del if.
                return RedirectToAction("ResultadoReserva", "Reserva", new { nombreCliente = nombreReserva + " " + apellidoReserva, correoElectronico = correoReserva, numeroReserva = g.ToString(), resultadoReserva = "1",precio = precio });
            } catch {
                return RedirectToAction("Index", "Home");
            }//Fin del try-catch.
        }//Fin del método Reserva.

        public double promocion(DateTime fechaEntrada, DateTime fechaSalida, int tipoHabitacion, double tarifa) {
            TimeSpan ts = fechaSalida - fechaEntrada;
            int diferencia = ts.Days;
            int mesFI = fechaEntrada.Month;
            int mesFf = fechaSalida.Month;
            double tarifaInicial = tarifa;
            tarifa = tarifa * diferencia;

            if ((mesFI >= 2 && mesFI <= 6) && (mesFf >= 2 && mesFf <= 6)) {
                if (tipoHabitacion == 2) {
                    tarifa = tarifa - (tarifa * 0.10);
                } else if (tipoHabitacion == 3) {
                    tarifa = tarifa - (tarifa * 0.20);
                }
            } else if ((mesFI >= 9 && mesFI <= 11) && (mesFf >= 9 && mesFf <= 11)) {
                if (diferencia >= 3 && diferencia < 5) {
                    tarifa = tarifa - tarifaInicial;
                } else if (diferencia >= 5) {
                    tarifa = tarifa - (tarifaInicial * 2);
                } else {
                    System.Console.Write("Difference in days: {0} ", diferencia);
                }
            } else {
                System.Console.Write("No esta en mes de promocion");
            }
            return tarifa;
        }//Fin del método promoción. 

        public ActionResult ResultadoReserva(string nombreCliente, string correoElectronico, string numeroReserva, string resultadoReserva,double precio) {
            ViewData["Cliente"] = nombreCliente;
            ViewData["Correo"] = correoElectronico;
            ViewData["NumeroReserva"] = numeroReserva;
            ViewData["Resultado"] = resultadoReserva;
            ViewData["Precio"] = precio;
            return View();
        }//Fin de la función ResultadoReserva.

        [HttpPost]
        public async Task<ActionResult> Consulta(string tarjeta) {
            List<TSH_Reserva> listaReservas = new List<TSH_Reserva>();
            Respuesta<List<TSH_Reserva>> respuesta = new Respuesta<List<TSH_Reserva>>();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync("api/TSH_Reserva/");
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<List<TSH_Reserva>>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    listaReservas = respuesta.valorRetorno;
                }//Fin del if.
            } catch (Exception ex){
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.
            string resultado = " / / ";

            foreach (TSH_Reserva reserva in listaReservas) {
                if (reserva.TSH_Cliente.TC_Tarjeta_TSH_Cliente.Equals(tarjeta)) {
                    resultado = reserva.TSH_Cliente.TC_Nombre_TSH_Cliente + "/" + reserva.TSH_Cliente.TC_Apellidos_TSH_Cliente + "/" + reserva.TSH_Cliente.TC_Correo_TSH_Cliente;
                }//Fin del if.
            }//Fin del foreach.
            return Json(resultado);
        }//CargarDatosCliente

        [HttpPost]
        public async Task<ActionResult> ObtenerReservas()
        {
            List<TSH_Reserva> listaReservas = new List<TSH_Reserva>();
            Respuesta<List<TSH_Reserva>> respuesta = new Respuesta<List<TSH_Reserva>>();
            try
            {
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
            return View("ListadoReservas", listaReservas);
        }//CargarDatosCliente

        public async Task<ActionResult> ObtenerReservas()
        {
            List<TSH_Reserva> listaReservas = new List<TSH_Reserva>();
            Respuesta<List<TSH_Reserva>> respuesta = new Respuesta<List<TSH_Reserva>>();
            try
            {
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
            return View("ListadoReservas", listaReservas);
        }//CargarDatosCliente

    }//Fin de la clase ReservaController.
}//Fin del namespace.