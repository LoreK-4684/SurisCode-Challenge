using Reservas.ServiceLayer.Masks;

namespace Reservas.ServiceLayer.Responses
{
    public class GetServiciosResponse
    {
       public IEnumerable<Servicio> Servicios { get; set; }
    }

}
