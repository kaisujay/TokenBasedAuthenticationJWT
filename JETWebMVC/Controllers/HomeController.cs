using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JETWebMVC.Controllers
{
    public class HomeController : Controller
    {
        private static string WebAPIURL = "http://localhost:4074/";
        // GET: Home
        public async Task<ActionResult> Index()
        {
            var tokenBased = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebAPIURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var responseMessage = await client.GetAsync(requestUri: "Account/ValidLogin?userName=admin&userPassword=admin");
                var responseMessage = await client.GetAsync("api/Token?UserName=admin&Password=123");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    tokenBased = JsonConvert.DeserializeObject<string>(resultMessage);
                    Session["TokenNumber"] = tokenBased;
                    Session["UserName"] = "admin";
                }
            }
            return Content(tokenBased);
        }

        public async Task<ActionResult> GetEmployee()
        {
            string ReturnMessage = String.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebAPIURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
                    Session["TokenNumber"].ToString() + ":" + Session["UserName"]);
                var responseMessage = await client.GetAsync("api/Token");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    ReturnMessage = JsonConvert.DeserializeObject<string>(resultMessage);
                }
            }
            return Content(ReturnMessage);
        }
    }
}