using Sem12.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Sem12.Controllers
{
    [JwtAuthenticationFilter]
    [EnableCors(origins:"*", methods:"*", headers:"*")]
    public class MensajesController : ApiController
    {
        [HttpGet]
        [Route("ObtenerMensaje")]
        public IHttpActionResult ObtenerMensajeSeguro()
        {
            return Ok(new { Mesanje = "Este es un canal seguro por JWT" });
        }
    }
}
