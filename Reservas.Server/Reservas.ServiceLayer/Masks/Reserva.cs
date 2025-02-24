using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservas.ServiceLayer.Masks
{
    public class Reserva
    {
        public DateTime Fecha { get; internal set; }
        public string Cliente { get; internal set; }
        public string Servicio { get; internal set; }
    }
}
