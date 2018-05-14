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
    public class AdminController : ConfigController {

        public ActionResult Home() {
            Session["Usuario"] = "1";
            return View();
        }//Fin del método Home.

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


        public static byte[] StrToByteArray(string str)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(str);
        }


        [HttpPost]
        public async Task<ActionResult> actualizarPaginaHome(int id, string descripcion, string imagen) {
            Respuesta<TSH_Pagina> respuesta = new Respuesta<TSH_Pagina>();
            TSH_Pagina pagina = new TSH_Pagina();
            try {

                byte[] image = StrToByteArray(imagen);
               
                pagina.TN_Identificador_TSH_Pagina = id;
                pagina.TC_Descripcion_TSH_Pagina = descripcion;
                pagina.TSH_Pag_Home.TI_Imagen_TSH_Pag_Home = image;

                String jsonContent = JsonConvert.SerializeObject(pagina);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                ByteArrayContent byteArrayContent = new ByteArrayContent(buffer);
                byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage responseWAPI = await webAPI.PutAsync(String.Concat("api/TSH_Pagina"), byteArrayContent);
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Pagina>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    pagina = respuesta.valorRetorno;
                }//Fin del if.

                if (respuesta.resultado == 1)
                    ViewBag.Message = "Los cambios en la pagina Inicio se realizaron exitosamente.";
                else
                    ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";

                return View("Home");
            } catch {
                ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";
                return View("Home");
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
                    pagina = respuesta.valorRetorno;
                }//Fin del if.
                if (respuesta.resultado == 1)
                    ViewBag.Message = "Los cambios en la pagina Sobre Nosotros se realizaron exitosamente.";
                else
                    ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";
                return View("Home");
            } catch {
                ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";
                return View("Home");
            }//Try-catch.
        }//Fin del método ActualizarSobreNosotros.

        public async Task<ActionResult> ComoLlegar()
        {
            TSH_Pagina comoLlegar = new TSH_Pagina();
            Respuesta<TSH_Pagina> respuesta = new Respuesta<TSH_Pagina>();
            try
            {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync(String.Concat("api/TSH_Pagina/", 6));
                if (responseWAPI.IsSuccessStatusCode)
                {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Pagina>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    comoLlegar = respuesta.valorRetorno;
                }//Fin del if.
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            return View(comoLlegar);
        }//ComoLlegar

        [HttpPost]
        public async Task<ActionResult> ActualizarComoLlegar(int id, string descripcion)
        {
            Respuesta<TSH_Pagina> respuesta = new Respuesta<TSH_Pagina>();
            TSH_Pagina pagina = new TSH_Pagina();
            try
            {
                pagina.TN_Identificador_TSH_Pagina = id;
                pagina.TC_Descripcion_TSH_Pagina = descripcion;
                String jsonContent = JsonConvert.SerializeObject(pagina);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                ByteArrayContent byteArrayContent = new ByteArrayContent(buffer);
                byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage responseWAPI = await webAPI.PutAsync(String.Concat("api/TSH_Pagina"), byteArrayContent);
                if (responseWAPI.IsSuccessStatusCode)
                {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Pagina>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    pagina = respuesta.valorRetorno;
                }//Fin del if.
                if (respuesta.resultado == 1)
                    ViewBag.Message = "Los cambios en la página ¿Cómo llegar? se realizaron exitosamente.";
                else
                    ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";
                return View("Home");
            }
            catch
            {
                ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";
                return View("Home");
            }//Try-catch.
        }//ActualizarComoLlegar
    }//Fin de la clase AdminController.
}//Fin del namespace.