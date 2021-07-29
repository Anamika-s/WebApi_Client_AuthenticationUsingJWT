using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebClient.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
 

namespace WebClient.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _Configure { get; set; }
        string apiBaseUrl = "";

        public LoginController(IConfiguration configuration)
        {

            _Configure = configuration;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }
        public IActionResult Login()
        {
            UserViewModel user = new UserViewModel();
            return View(user);
        }

        public class JWT
        {
            public string Token { get; set; }
        }
        [HttpPost]
        public IActionResult Login(UserViewModel user)
        {
            HttpClient client = new HttpClient();

            var token = string.Empty;
              StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
           
            string endpoint = "https://localhost:44371/api/Authentication";
            client.BaseAddress = new Uri(endpoint);
            var contentType = new MediaTypeWithQualityHeaderValue
("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType); 
            var Response = client.PostAsync(endpoint, content);
            var result = Response.Result;

            if (result.IsSuccessStatusCode)
            {
                var stringJWT = result.Content.ReadAsStringAsync().Result;
              JWT jwt = JsonConvert.DeserializeObject
<JWT>(stringJWT);
                HttpContext.Session.SetString("token", jwt.Token);

                ViewBag.Message = "User logged in successfully!";
                //HttpContext.Session["token"] = token.ToString();
                //return View();
                
                return RedirectToAction("Index", "Employee");

            }
            else
                return View();
        }
    }

}
