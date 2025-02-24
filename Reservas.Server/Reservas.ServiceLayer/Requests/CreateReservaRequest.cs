namespace Reservas.ServiceLayer.Requests
{
    public class CreateReservaRequest
    {
        public string Servicio { get; set; }
        
        public string Fecha { get; set; }

        public string Cliente { get; set; }
    }

}
