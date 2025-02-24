using Reservas.ServiceLayer.Masks;

namespace Reservas.ServiceLayer.Responses
{
    public class GetReservasResponse
    {
       public IEnumerable<Reserva> Reservas { get; set; }
    }

}
