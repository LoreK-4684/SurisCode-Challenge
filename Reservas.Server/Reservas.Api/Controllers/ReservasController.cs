using Microsoft.AspNetCore.Mvc;
using Reservas.ServiceLayer.Requests;
using Reservas.ServiceLayer.Responses;
using Reservas.ServiceLayer.Services;
using Reservas.ServiceLayer.Support;

namespace Reservas.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservasController : MyController
    {
        private IReservasService _service;

        public ReservasController(IReservasService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetReservas")]
        public ActionResult GetReservas()
        {
            return Secure(() => _service.Get());
        }

        [HttpPost]
        [Route("CreateReserva")]
        public ActionResult CreateReserva([FromBody]CreateReservaRequest request)
        {
            return Secure(() => _service.Create(request));
        }

    }
}
