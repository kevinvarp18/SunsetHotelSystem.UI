using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SunsetHotelSystem.UI.Controllers {
    public class ConfigController : Controller {
        #region
        public HttpClient webAPI;
        string urlWebAPI = System.Configuration.ConfigurationManager.AppSettings["URLWebAPI"].ToString();
        #endregion

        public ConfigController() {
            webAPI = new HttpClient();
            webAPI.BaseAddress = new System.Uri(urlWebAPI);
            webAPI.DefaultRequestHeaders.Accept.Clear();
            webAPI.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}