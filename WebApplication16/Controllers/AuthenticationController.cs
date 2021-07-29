using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApplication16.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Web.Http;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System.Security.Claims;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using WebApplication16.Services;

namespace WebApplication16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly StudentDbContext _context;
        private IConfiguration _config;
        private readonly ITokenGeneratorService service;
        public AuthenticationController(StudentDbContext context, IConfiguration config, ITokenGeneratorService _service)
        {
            _context = context;
            _config = config;
            service = _service;
        }

        //[AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] User User)
        {
            //IActionResult response = Unauthorized();
            var user = Authenticate(User);
            if (user != null)
            {
                var tokenString = service.GenerateJSONWebToken(user);
                return Ok(new { token = tokenString });
            }
            else
                return BadRequest("Invalid User");
          
        }

        //[HttpGet]
        //public string GetToken([FromBody] User user)
        //{
            
        //    return GenerateJSONWebToken(user);
        //}
       

        public User Authenticate(User user)
        {
            User obj = _context.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            return obj;
            
        }
    }
}