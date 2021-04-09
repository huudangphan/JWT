using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticateServies _authenticateServies;
        public AuthenticationController(IAuthenticateServies authenticateServies)
        {
            _authenticateServies = authenticateServies;
        }
        [HttpPost]

        public IActionResult Post([FromBody] User model)
        {
            var user = _authenticateServies.Authenticate(model.username, model.password);
            if (user == null)
                return BadRequest(new {message= "Username or password incorrect" });
            return Ok(user);

        }
    }
}
