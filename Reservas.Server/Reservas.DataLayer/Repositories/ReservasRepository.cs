using Reservas.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservas.DataLayer.Repositories
{
    public interface IReservasRepository
    {
        Reserva Create(string Servicio, DateTime fecha, string usuario);
        bool Exists(DateTime dateTime);
        bool Exists(DateTime fecha, string cliente);
        IQueryable<Reserva> GetAll();
        IQueryable<Reserva> GetByDate(DateTime date);
    }

    public class ReservasRepository : IReservasRepository
    {
        private ReservasDbContext _dbContext;

        public ReservasRepository(ReservasDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public Reserva Create(string Servicio, DateTime fecha, string usuario)
        {
            var reserva = new Reserva();
            reserva.Fecha = new DateTime(fecha.Year, fecha.Month, fecha.Day, fecha.Hour, 0, 0);
            reserva.Cliente = usuario;
            reserva.Servicio = Servicio;

            _dbContext.Reservas.Add(reserva);
            _dbContext.SaveChanges();

            return reserva;
        }

        public bool Exists(DateTime dt)
        {
            var dateTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
            return _dbContext.Reservas.Any(x => dateTime == x.Fecha);
        }

        public bool Exists(DateTime fecha, string cliente)
        {
            return GetByDate(fecha).Any(x => x.Cliente == cliente);
        }

        public IQueryable<Reserva> GetAll()
        {
            return _dbContext.Reservas.AsQueryable();
        }

        public IQueryable<Reserva> GetByDate(DateTime date)
        {
            DateTime init = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime end = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

            return GetAll().Where(x => init <= x.Fecha && end >= x.Fecha);
        }
    }
}
