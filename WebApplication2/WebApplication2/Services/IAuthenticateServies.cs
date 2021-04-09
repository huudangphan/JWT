using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
namespace WebApplication2.Services
{
    public interface IAuthenticateServies
    {
        User Authenticate(string username, string password);
    }
}
