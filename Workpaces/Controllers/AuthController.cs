using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Workpaces.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;


namespace Workpaces.Controllers
{
    public class AuthController : ApiController
    {
        private const string SecretKey = "DGFHGFGFDGMNKFDNGVKFVKV4566765KJEWRFREGV23445";

        private string GenerarToken(string usuario)
        {
            var key = Encoding.UTF8.GetBytes(SecretKey);
            var PreToken = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, usuario) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var Token = tokenHandler.CreateToken(PreToken);
            return tokenHandler.WriteToken(Token);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Login")]
        public IHttpActionResult Login([FromBody] LoginViewModel login)
        {
            // Validacion del usuario de forma correcta
            
            if (login.Email == "admin@gmail.com" && login.Password== "Admin")
            {
                var Token = GenerarToken(login.Email);
                return Ok(new { Token = Token });
               
            }
            return Unauthorized();
        }
    }
}
