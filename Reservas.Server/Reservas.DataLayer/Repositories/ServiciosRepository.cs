using Reservas.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservas.DataLayer.Repositories
{
    public interface IServiciosRepository
    {
        bool Exists(string servicio);
        IQueryable<string> GetAll();
    }

    public class ServiciosRepository : IServiciosRepository
    {

        private static readonly string[] ServiciosNames = new[]
     {
            "Cambio de Pantalla", "Formateo"
        };

        public ServiciosRepository() 
        {
        }

        public bool Exists(string servicio)
        {
            return GetAll().Any(x => x == servicio);
        }

        public IQueryable<string> GetAll()
        {
            return ServiciosNames.AsQueryable();

        }

    }
}
