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
using System.IO;

namespace SunsetHotelSystem.UI.Controllers {
    public class ModificarPaginasController : ConfigController {

        public ActionResult ModificarPaginas() {
            return View();
        }//Fin del método ModificarPaginas.

        public async Task<ActionResult> PaginaHome() {
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
        }//Fin de la funcion PaginaHome.

        [HttpPost]
        public async Task<ActionResult> actualizarPaginaHome(int id, string descripcion, HttpPostedFileBase imagen) {
            Respuesta<TSH_Pagina> respuesta = new Respuesta<TSH_Pagina>();
            TSH_Pagina pagina = new TSH_Pagina();
            pagina.TSH_Pag_Home = new TSH_Pag_Home();

            try {
                if (imagen.ContentLength > 0) {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(imagen.InputStream)) {
                        imageData = binaryReader.ReadBytes(imagen.ContentLength);
                    }
                    pagina.TSH_Pag_Home.TI_Imagen_TSH_Pag_Home = imageData;
                }//Fin del if.
            } catch (Exception ex) {
            }//Fin del try-catch.

            Guid g = Guid.NewGuid();
            pagina.TSH_Pag_Home.TN_Id_Imagen_TSH_Pag_Home = g;
            pagina.TN_Identificador_TSH_Pagina = id;
            pagina.TSH_Pag_Home.TN_Identificador_TSH_Pag_Home = id;
            pagina.TC_Descripcion_TSH_Pagina = descripcion;

            try {
                String jsonContent = JsonConvert.SerializeObject(pagina);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                ByteArrayContent byteArrayContent = new ByteArrayContent(buffer);
                byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage responseWAPI = await webAPI.PutAsync(String.Concat("api/TSH_Pagina"), byteArrayContent);
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Pagina>>(responseWAPI.Content.ReadAsStringAsync().Result);
                }//Fin del if.

                if (respuesta.resultado == 1)
                    ViewBag.Message = "Los cambios en la pagina Inicio se realizaron exitosamente.";
                else
                    ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";

                return View("../Administrador/Home");
            } catch {
                ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";
                return View("../Administrador/Home");
            }//Try-catch.
        }//Fin del método guardarCambios.

        public async Task<ActionResult> PaginaSobreNosotros() {
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
        }//Fin de la funcion PaginaSobreNosotros.

        [HttpPost]
        public async Task<ActionResult> ActualizarSobreNosotros(int id, string descripcion) {
            Respuesta<TSH_Pagina> respuesta = new Respuesta<TSH_Pagina>();
            TSH_Pagina pagina = new TSH_Pagina();
            try {
                pagina.TN_Identificador_TSH_Pagina = id;
                pagina.TC_Descripcion_TSH_Pagina = descripcion;
                String jsonContent = JsonConvert.SerializeObject(pagina);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                ByteArrayContent byteArrayContent = new ByteArrayContent(buffer);
                byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage responseWAPI = await webAPI.PutAsync(String.Concat("api/TSH_Pagina"), byteArrayContent);
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Pagina>>(responseWAPI.Content.ReadAsStringAsync().Result);
                }//Fin del if.

                if (respuesta.resultado == 1)
                    ViewBag.Message = "Los cambios en la pagina Sobre Nosotros se realizaron exitosamente.";
                else
                    ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";
                return View("../Administrador/Home");
            } catch {
                ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";
                return View("../Administrador/Home");
            }//Try-catch.
        }//Fin del método ActualizarSobreNosotros.

        public async Task<ActionResult> PaginaComoLlegar() {
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
        }//Fin del método PaginaComoLlegar.

        [HttpPost]
        public async Task<ActionResult> ActualizarComoLlegar(int id, string descripcion) {
            Respuesta<TSH_Pagina> respuesta = new Respuesta<TSH_Pagina>();
            TSH_Pagina pagina = new TSH_Pagina();
            try {
                pagina.TN_Identificador_TSH_Pagina = id;
                pagina.TC_Descripcion_TSH_Pagina = descripcion;
                String jsonContent = JsonConvert.SerializeObject(pagina);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                ByteArrayContent byteArrayContent = new ByteArrayContent(buffer);
                byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage responseWAPI = await webAPI.PutAsync(String.Concat("api/TSH_Pagina"), byteArrayContent);
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Pagina>>(responseWAPI.Content.ReadAsStringAsync().Result);
                }//Fin del if.

                if (respuesta.resultado == 1)
                    ViewBag.Message = "Los cambios en la página ¿Cómo llegar? se realizaron exitosamente.";
                else
                    ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";
                return View("../Administrador/Home");
            } catch {
                ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";
                return View("../Administrador/Home");
            }//Try-catch.
        }//Fin del método ActualizarComoLlegar.

        public async Task<ActionResult> PaginaFacilidades() {
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
        }//Fin de la funcion PaginaFacilidades.

        [HttpPost]
        public async Task<ActionResult> ActualizarFacilidades(FormCollection collection) {
            int bandera = 1;
            int contador = 1;
            TSH_Pagina pagina = new TSH_Pagina();
            pagina.TN_Identificador_TSH_Pagina = 7;
            pagina.TC_Descripcion_TSH_Pagina = collection.GetValue("descripcionPagina").AttemptedValue.ToString();
            Respuesta<TSH_Pagina> respuestaPagina = new Respuesta<TSH_Pagina>();

            try
            {
                String jsonContent = JsonConvert.SerializeObject(pagina);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                ByteArrayContent byteArrayContent = new ByteArrayContent(buffer);
                byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage responseWAPI = await webAPI.PutAsync(String.Concat("api/TSH_Pagina"), byteArrayContent);
                if (responseWAPI.IsSuccessStatusCode)
                {
                    respuestaPagina = JsonConvert.DeserializeObject<Respuesta<TSH_Pagina>>(responseWAPI.Content.ReadAsStringAsync().Result);
                }//Fin del if.
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            if (respuestaPagina.resultado == 0)
                bandera = 0;

            List<TSH_Pag_Facilidades> listaFacilidades = new List<TSH_Pag_Facilidades>();
            List<Respuesta<TSH_Pag_Facilidades>> respuestas = new List<Respuesta<TSH_Pag_Facilidades>>();
            int totalFacilidades = int.Parse(collection.GetValue("facilidadesTotal").AttemptedValue.ToString());
            int nuevasFacilidades = int.Parse(collection.GetValue("nuevasFacilidades").AttemptedValue.ToString());

            for (int i = 1; i <= (totalFacilidades + nuevasFacilidades); i++)
            {
                HttpPostedFileBase imagen = Request.Files["imagen" + i];
                TSH_Pag_Facilidades facilidad = new TSH_Pag_Facilidades();

                try
                {
                    if (imagen.ContentLength > 0)
                    {
                        byte[] imageData = null;
                        using (var binaryReader = new BinaryReader(imagen.InputStream))
                        {
                            imageData = binaryReader.ReadBytes(imagen.ContentLength);
                        }
                        facilidad.TI_Imagen_TSH_Pag_Facilidades = imageData;
                    }//Fin del if.
                }
                catch (Exception ex)
                {
                }//Fin del try-catch.

                Guid g = Guid.NewGuid();
                facilidad.TN_Identificador_TSH_Pag_Facilidades = 7;
                facilidad.TN_Borrado_TSH_Pag_Facilidades = int.Parse(collection.GetValue("borrado" + i).AttemptedValue.ToString());
                facilidad.TN_IdentificadorNumFac_TSH_Pag_Facilidades = int.Parse(collection.GetValue("id" + i).AttemptedValue.ToString());
                facilidad.TC_TituloFacilidad_TSH_Pag_Facilidades = collection.GetValue("tituloFacilidad" + i).AttemptedValue.ToString();
                facilidad.TC_Descripcion_TSH_Pag_Facilidades = collection.GetValue("descripcion" + i).AttemptedValue.ToString();
                facilidad.TN_Id_Imagen_TSH_Pag_Facilidades = g;

                listaFacilidades.Add(facilidad);
            }//Fin del for.

            try
            {
                foreach (TSH_Pag_Facilidades facilidad in listaFacilidades)
                {
                    String jsonContent = JsonConvert.SerializeObject(facilidad);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                    ByteArrayContent byteArrayContent = new ByteArrayContent(buffer);
                    byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage responseWAPI;

                    if (contador <= totalFacilidades)
                        responseWAPI = await webAPI.PutAsync(String.Concat("api/TSH_Pag_Facilidades"), byteArrayContent);
                    else
                        responseWAPI = await webAPI.PostAsync(String.Concat("api/TSH_Pag_Facilidades"), byteArrayContent);

                    if (responseWAPI.IsSuccessStatusCode)
                    {
                        respuestas.Add(JsonConvert.DeserializeObject<Respuesta<TSH_Pag_Facilidades>>(responseWAPI.Content.ReadAsStringAsync().Result));
                    }//Fin del if.
                    contador++;
                }//Fin del foreach.
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            foreach (Respuesta<TSH_Pag_Facilidades> respuesta in respuestas)
            {
                if (respuesta.resultado == 0)
                    bandera = 0;
            }//Fin del foreach.

            if (bandera == 1)
                ViewBag.Message = "Los cambios en la pagina Facilidades se realizaron exitosamente.";
            else
                ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";

            return View("../Administrador/Home");
        }//Fin de la funcion PaginaFacilidades.
    }//Fin de la clase ModificarPaginasController.
}//Fin del namespace.