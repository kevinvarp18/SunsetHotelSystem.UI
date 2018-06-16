using SunsetHotelSystem.Dominio.Entidades;
using SunsetHotelSystem.Dominio.UTL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SunsetHotelSystem.UI.Controllers {
    public class AdministradorController : ConfigController {
        public ActionResult Home() {
            if (Session["Usuario"].Equals("1"))
                return View();
            else
                return RedirectToAction("Login");
        }//Fin del método Home.

        public ActionResult Login() {
            if (Session["Usuario"].Equals("0"))
                return View();
            else
                return RedirectToAction("Home");
        }//Fin del método Login.

        public ActionResult Logout() {
            Session["Usuario"] = "0";
            return RedirectToAction("Login");
        }//Fin del método Logout.

        [HttpPost]
        public async Task<ActionResult> AutenticarUsuario(string usuario, string contrasena) {
            TSH_Administrador administrador = new TSH_Administrador();
            administrador.TC_Correo_TSH_Administrador = usuario;
            administrador.TC_contrasenia_TSH_Administrador = contrasena;
            Respuesta<TSH_Administrador> respuesta = new Respuesta<TSH_Administrador>();
            try {
                String jsonContent = JsonConvert.SerializeObject(administrador);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                ByteArrayContent byteArrayContent = new ByteArrayContent(buffer);
                byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage responseWAPI = await webAPI.PostAsync("api/TSH_Administrador/", byteArrayContent);
                if (responseWAPI.IsSuccessStatusCode) {
                    respuesta = JsonConvert.DeserializeObject<Respuesta<TSH_Administrador>>(responseWAPI.Content.ReadAsStringAsync().Result);
                }//Fin del if.
            } catch (Exception ex) {
                System.Console.Write(ex.ToString());
            }//Fin del try-catch.

            if (respuesta.resultado == 1) {
                Session["Usuario"] = "1";
                return RedirectToAction("Home");
            } else {
                return RedirectToAction("Login");
            }//Verifica si el usuario fue autenticado correctamente.
        }//Fin del método AutenticarUsuario.
    }//Fin de la clase AdminController.
}//Fin del namespace.