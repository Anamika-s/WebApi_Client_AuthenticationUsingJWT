using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication16.Models;
namespace WebApplication16.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private IConfiguration _config;
        public TokenGeneratorService(IConfiguration config)
        {
            _config = config;
        }
        

        string ITokenGeneratorService.GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name , user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: "CustomerWebApi",
              claims: claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
