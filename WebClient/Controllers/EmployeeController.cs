using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebClient.Models;

namespace WebClient.Controllers
{
    //[Authorize(JwtBearerDefaults.AuthenticationScheme)]
   // [Authorize]
 
        public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private IConfiguration _Configure { get;  set; }
        string apiBaseUrl = "";
        public EmployeeController(ILogger<EmployeeController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
           
            _logger = logger;
             _Configure = configuration;

             apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        
        public async Task<IActionResult> Index()
        {
            if (TempData["msg"] != null)
                ViewBag.msg = "<script> alert('Record Inserted'); </script>";
            List<EmployeeViewModel> students = new List<EmployeeViewModel>();
            using (var client = new HttpClient())
            {
                //Send HTTP requests from here. 
                // string endpoint = "https://localhost:44371/api/";
                client.DefaultRequestHeaders.Clear();
                var contentType = new MediaTypeWithQualityHeaderValue
("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
            HttpContext.Session.GetString("token"));
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44371/api/Employees");


                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    students = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(jsonString);
                }
                return View(students);

            }
        }




        public async Task<IActionResult> Details(int Id)
        {
            EmployeeViewModel student = new EmployeeViewModel();
            using (var client = new HttpClient())
            {
                //Send HTTP requests from here. 
               // string endpoint = "https://localhost:44371/api/";
                client.BaseAddress = new Uri(apiBaseUrl);

                //GET Method  
                HttpResponseMessage response = await client.GetAsync($"EMployees/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    student = JsonConvert.DeserializeObject<EmployeeViewModel>(jsonString);
                    return View(student);
                }
                else
                {
                    ViewBag.msg = response.StatusCode;
                    return View();
                }


            }
        }

        public IActionResult Create()
        {
            EmployeeViewModel employee = new EmployeeViewModel();
            return View(employee);
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
        {
            HttpClient client = new HttpClient();
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                string endpoint = "https://localhost:44371/api/employees";
                client.BaseAddress = new Uri(endpoint);
                var Response = client.PostAsync(endpoint, content);

                Response.Wait();
                var result = Response.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["msg"] = "Record Inserted";
                    return RedirectToAction("Index");
                }

                else if (Response.Result.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("Id", "Id Already Exist");
                    return View();
                }
                else
                {
                    TempData["msg"] = Response.Result.StatusCode;
                    return RedirectToAction("Index");
                }

            }
        }

        public async Task<IActionResult> Delete(int Id)
        {
            EmployeeViewModel student = new EmployeeViewModel();
            using (var client = new HttpClient())
            {
                //Send HTTP requests from here. 
                string endpoint = "https://localhost:44371/api/";
                client.BaseAddress = new Uri(endpoint);

                //GET Method  
                HttpResponseMessage response = await client.GetAsync($"EMployees/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    student = JsonConvert.DeserializeObject<EmployeeViewModel>(jsonString);
                    return View(student);
                }
                else
                {
                    ViewBag.msg = response.StatusCode;
                    return View();
                }

            }
        }


        [HttpPost]
        public  async Task<IActionResult> Delete(int id, EmployeeViewModel employee)
        {
            using (var client = new HttpClient())
            {
                //Send HTTP requests from here. 
                string endpoint = "https://localhost:44371/api/";
                client.BaseAddress = new Uri(endpoint);
                HttpResponseMessage response = await client.DeleteAsync($"employees/{id}");

            }
                TempData["msg"] = "Record Deleted";
            return RedirectToAction("Index");

        }
    }
}