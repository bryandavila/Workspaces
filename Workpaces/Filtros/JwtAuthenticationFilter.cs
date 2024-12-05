using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Sem12.Filtros
{
    public class JwtAuthenticationFilter : AuthorizationFilterAttribute
    {
        private const string SecretKey = "DGFHGFGFDGMNKFDNGVKFVKV4566765KJEWRFREGV23445";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;

            if ((authHeader==null || authHeader.Scheme != "Bearer" || string.IsNullOrEmpty(authHeader.Parameter)))
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                return;
            }

            var Token = authHeader.Parameter;

            try
            {
                var key = Encoding.UTF8.GetBytes(SecretKey);
                var tokenHandler = new JwtSecurityTokenHandler();

                tokenHandler.ValidateToken(Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience= false ,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var usename = jwtToken.Claims.First(x => x.Type == "unique_name").Value;

                var idetity = new GenericIdentity(usename);
                actionContext.RequestContext.Principal = new GenericPrincipal(idetity, null);
            }
            catch
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}