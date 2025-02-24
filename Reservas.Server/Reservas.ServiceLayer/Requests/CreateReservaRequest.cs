namespace Reservas.ServiceLayer.Requests
{
    public class CreateReservaRequest
    {
        public string Servicio { get; set; }
        
        public DateTime Fecha { get; set; }

        public string Cliente { get; set; }
    }

}
