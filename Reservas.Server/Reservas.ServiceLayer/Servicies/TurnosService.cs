
using Reservas.DataLayer.Repositories;
using Reservas.ServiceLayer.Masks;
using Reservas.ServiceLayer.Requests;
using Reservas.ServiceLayer.Responses;

namespace Reservas.ServiceLayer.Services
{
    public interface ITurnosService
    {
        TurnoDisponibleByFechaResponse DisponiblesByFecha(TurnoDisponibleByFechaRequest request);
    }

    public class TurnosService : ITurnosService
    {
        private IReservasRepository _repository;

        public TurnosService(IReservasRepository repository) 
        {
            _repository = repository;
        }

        public TurnoDisponibleByFechaResponse DisponiblesByFecha(TurnoDisponibleByFechaRequest request)
        {
            var res = new TurnoDisponibleByFechaResponse();

            var reservasByFecha = _repository.GetByDate(request.Fecha);

            // Turnos Disponibles de 9hs a 18hs

            var hsRange = Enumerable.Range(9,9).ToList();

            var hsOcupadas = reservasByFecha.Select(x => x.Fecha.Hour).ToList();
            var hsDisponibles = hsRange.Where(x => !hsOcupadas.Contains(x));

            res.Fecha = request.Fecha;
            res.TurnosDisponibles = hsDisponibles.Select(x => new TurnoDisponible() { Hora = x });

            return res;
        }
    }
}
