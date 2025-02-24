using Reservas.ServiceLayer.Masks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservas.ServiceLayer.Responses
{
    public class TurnoDisponibleByFechaResponse
    {
        // Fecha sin h,m,s
        public DateTime Fecha { get; set; }

        public IEnumerable<TurnoDisponible> TurnosDisponibles { get; set; }
    }

}
