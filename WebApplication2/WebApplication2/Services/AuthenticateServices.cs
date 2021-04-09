using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WebApplication2.Services
{
    public class AuthenticateServices : IAuthenticateServies
    {
        private readonly AppSettings _appSettings;
        public AuthenticateServices(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        private List<User> users = new List<User>()
        {
            new User{id=1,username="dang",password="dang123"}
        };
        public User Authenticate(string username, string password)
        {
            var user = users.SingleOrDefault(x => x.username == username && x.password == password);
            if (user == null)
                return null;

            var tokenHandle = new JwtSecurityTokenHandler();
            var key = Encoding.UTF32.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.id.ToString()),
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim(ClaimTypes.Version,"V3.1")
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandle.CreateToken(tokenDescriptor);
            user.Token= tokenHandle.WriteToken(token);
            user.password = null;
            return user;


        }
    }
}
