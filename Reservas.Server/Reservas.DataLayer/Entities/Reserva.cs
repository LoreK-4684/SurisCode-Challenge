using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservas.DataLayer.Entities
{
    public class Reserva
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Servicio { get; set; }
        public int Id { get; set; }
    }
}
