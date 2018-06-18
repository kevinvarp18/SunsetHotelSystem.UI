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
using System.IO;

namespace SunsetHotelSystem.UI.Controllers {
    public class PublicidadController : ConfigController {
        public async Task<ActionResult> Publicidad() {
            List<TSH_Publicidad> listaPublicidad = new List<TSH_Publicidad>();
            Respuesta<List<TSH_Publicidad>> respuesta = new Respuesta<List<TSH_Publicidad>>();
            try {
                HttpResponseMessage responseWAPI = await webAPI.GetAsync("api/TSH_Publicidad");
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<List<TSH_Publicidad>>>(responseWAPI.Content.ReadAsStringAsync().Result);
                    listaPublicidad = respuesta.valorRetorno;
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.
            return View(listaPublicidad);
        }//Fin del método Publicidad.

        public async Task<ActionResult> ActualizarPublicidad(FormCollection collection) {
            int bandera = 1;
            int contador = 1;

            List<TSH_Publicidad> listaPublicidad = new List<TSH_Publicidad>();
            List<Respuesta<TSH_Publicidad>> respuestas = new List<Respuesta<TSH_Publicidad>>();
            int totalPublicidad = int.Parse(collection.GetValue("publicidadTotal").AttemptedValue.ToString());
            int nuevaPublicidad = int.Parse(collection.GetValue("nuevaPublicidad").AttemptedValue.ToString());

            for (int i = 1; i <= (totalPublicidad + nuevaPublicidad); i++) {
                HttpPostedFileBase imagen = Request.Files["imagen" + i];
                TSH_Publicidad publicidad = new TSH_Publicidad();

                try {
                    if (imagen.ContentLength > 0) {
                        byte[] imageData = null;
                        using (var binaryReader = new BinaryReader(imagen.InputStream)) {
                            imageData = binaryReader.ReadBytes(imagen.ContentLength);
                        }
                        publicidad.TI_Imagen_TSH_Publicidad = imageData;
                    }//Fin del if.
                } catch {
                }//Fin del try-catch.

                Guid g = Guid.NewGuid();
                publicidad.TN_Borrado_TSH_Publicidad = int.Parse(collection.GetValue("borrado" + i).AttemptedValue.ToString());
                publicidad.TN_Identificador_TSH_Publicidad = int.Parse(collection.GetValue("id" + i).AttemptedValue.ToString());
                publicidad.TC_Direccion_Pagina_TSH_Publicidad = collection.GetValue("rutaPublicidad" + i).AttemptedValue.ToString();
                publicidad.TN_Id_Imagen_TSH_Publicidad = g;

                listaPublicidad.Add(publicidad);
            }//Fin del for.

            try {
                foreach (TSH_Publicidad publicidad in listaPublicidad) {
                    String jsonContent = JsonConvert.SerializeObject(publicidad);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                    ByteArrayContent byteArrayContent = new ByteArrayContent(buffer);
                    byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage responseWAPI;

                    if (contador <= totalPublicidad)
                        responseWAPI = await webAPI.PutAsync(String.Concat("api/TSH_Publicidad"), byteArrayContent);
                    else
                        responseWAPI = await webAPI.PostAsync(String.Concat("api/TSH_Publicidad"), byteArrayContent);

                    if (responseWAPI.IsSuccessStatusCode) {
                        respuestas.Add(JsonConvert.DeserializeObject<Respuesta<TSH_Publicidad>>(responseWAPI.Content.ReadAsStringAsync().Result));
                    }//Fin del if.
                    contador++;
                }//Fin del foreach.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            foreach (Respuesta<TSH_Publicidad> respuesta in respuestas) {
                if (respuesta.resultado == 0)
                    bandera = 0;
            }//Fin del foreach.

            if (bandera == 1)
                ViewBag.Message = "Los cambios en la publicidad se realizaron exitosamente.";
            else
                ViewBag.Message = "¡Oops! Ocurrió un error a la hora de realizar los cambios.";

            return View("../Administrador/Home");
        }//Fin del método ActualizarPublicidad.

    }//Fin de la clase PublicidadController.
}//Fin del namespace.