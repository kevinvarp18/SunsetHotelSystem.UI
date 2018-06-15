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
using System.Security.Permissions;

namespace SunsetHotelSystem.UI.Controllers {
    public class HabitacionesController : ConfigController {

        private Habitacion habitaciones;
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

            this.habitaciones = new Habitacion(listaTiposHabitacion, listaHabitaciones,listaHabitacionesReservadas);
            generarPDF();
            return View(this.habitaciones);
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

        public void generarPDF()
        {
            DateTime fechaActual = DateTime.Now;
            FileIOPermission f = new FileIOPermission(FileIOPermissionAccess.AllAccess, "C:\\Users\\santi\\Desktop");
            f.AllLocalFiles = FileIOPermissionAccess.Write;
            f.Demand();
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(@"C:\Users\santi\Desktop\EstadoHabitacionSunSetHotel(" + fechaActual.Day+"-"+ fechaActual.Month+"-"+fechaActual.Year+").pdf", FileMode.Create));

            // Se le coloca el título y el autor
            doc.AddTitle("Estado de las habitaciones");
            doc.AddCreator("Suntet Hotel System");

            // Abrimos el archivo
            doc.Open();
            // Se crea el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
    
            iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLDITALIC, BaseColor.DARK_GRAY);
            iTextSharp.text.Font _standardFont6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);

            iTextSharp.text.Font _standardFont3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, iTextSharp.text.Font.NORMAL, BaseColor.LIGHT_GRAY);
            iTextSharp.text.Font _standardFont4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font _standardFont5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            // Se escribe el encabezamiento en el documento
            doc.Add(new Paragraph("SunSet Hotel", _standardFont2));
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Paragraph("Reporte de habitaciones", _standardFont6));
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Paragraph("A continuación, se muestra el desglose del estado de las habitaciones en la presente fecha: "+ fechaActual.ToShortDateString() +" y a la hora: " + fechaActual.ToShortTimeString(), _standardFont6));
            doc.Add(Chunk.NEWLINE);

            // Se crean las tablas (en este caso 3)
            PdfPTable tblPrueba = new PdfPTable(3);
            tblPrueba.WidthPercentage = 100;

            // Se configura el título de las columnas de la tabla
            PdfPCell clNombrePrimera = new PdfPCell(new Phrase("Número de Habitación", _standardFont));
            clNombrePrimera.BorderWidth = 0;
            clNombrePrimera.BorderWidthBottom = 0.75f;

            PdfPCell clNombreSegunda = new PdfPCell(new Phrase("Tipo de habitación", _standardFont));
            clNombreSegunda.BorderWidth = 0;
            clNombreSegunda.BorderWidthBottom = 0.75f;

            PdfPCell clNombreTercera = new PdfPCell(new Phrase("Estado de la habitación", _standardFont));
            clNombreTercera.BorderWidth = 0;
            clNombreTercera.BorderWidthBottom = 0.75f;

            // se añade las celdas a la tabla
            tblPrueba.AddCell(clNombrePrimera);
            tblPrueba.AddCell(clNombreSegunda);
            tblPrueba.AddCell(clNombreTercera);


            foreach (var habitacion in this.habitaciones.Habitaciones)
            {

                clNombrePrimera = new PdfPCell(new Phrase(habitacion.TN_Numero_Habitacion_TSH_Habitacion.ToString(), _standardFont4));
                clNombrePrimera.BorderWidth = 0;

                if (habitacion.TN_Id_TipoH_TSH_Habitacion == 2)
                {
                    clNombreSegunda = new PdfPCell(new Phrase("Estándar", _standardFont4));
                    clNombreSegunda.BorderWidth = 0;
                }
                else
                {
                    clNombreSegunda = new PdfPCell(new Phrase("Suite", _standardFont4));
                    clNombreSegunda.BorderWidth = 0;
                }
                if (this.habitaciones.Reservadas.Count > 0)
                {
                    foreach (var reservas in this.habitaciones.Reservadas)
                    {
                        if (reservas.TN_Numero_Habitacion_TSH_Habitacion != habitacion.TN_Numero_Habitacion_TSH_Habitacion)
                        {
                            clNombreTercera = new PdfPCell(new Phrase("Disponible", _standardFont4));
                            clNombreTercera.BorderWidth = 0;
                        }
                        else
                        {
                            clNombreTercera = new PdfPCell(new Phrase("Ocupada", _standardFont4));
                            clNombreTercera.BorderWidth = 0;
                        }
                    }
                }
                else
                {
                    clNombreTercera = new PdfPCell(new Phrase("Disponible", _standardFont4));
                    clNombreTercera.BorderWidth = 0;
                }
                tblPrueba.AddCell(clNombrePrimera);
                tblPrueba.AddCell(clNombreSegunda);
                tblPrueba.AddCell(clNombreTercera);

            }
          
            // Finalmente, se añade la tabla al documento PDF y se cierra el documento
            doc.Add(tblPrueba);
            
            doc.Add(new Paragraph("-Fin del reporte SunSet Hotel System. Emitido el " + fechaActual.ToString(), _standardFont3));
            doc.Add(Chunk.NEWLINE);
   

            doc.Close();
            writer.Close();
        }//End generarPDF

    }//Fin de la clase HabitacionesController.
}//Fin de la namespace.