using Microsoft.AspNetCore.Mvc;
using Reservas.ServiceLayer.Responses;
using Reservas.ServiceLayer.Services;

namespace Reservas.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiciosController : MyController
    {
        private IServiciosService _service;

        public ServiciosController(IServiciosService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetServicios")]
        public ActionResult GetServicios()
        {
            return Secure(() => _service.Get());
        }
    }
}
