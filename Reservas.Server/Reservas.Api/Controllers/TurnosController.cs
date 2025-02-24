using Microsoft.AspNetCore.Mvc;
using Reservas.ServiceLayer.Requests;
using Reservas.ServiceLayer.Responses;
using Reservas.ServiceLayer.Services;

namespace Reservas.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TurnosController : MyController
    {
        private ITurnosService _service;

        public TurnosController(ITurnosService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("DisponiblesByFecha")]
        public ActionResult DisponiblesByFecha([FromBody]TurnoDisponibleByFechaRequest request)
        {
            return Secure(() => _service.DisponiblesByFecha(request));
        }

    }
}
