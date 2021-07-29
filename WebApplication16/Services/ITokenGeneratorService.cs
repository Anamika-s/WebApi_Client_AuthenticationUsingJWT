using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication16.Models;

namespace WebApplication16.Services
{
   public interface ITokenGeneratorService
    {
        string GenerateJSONWebToken(User user);
    }
}
