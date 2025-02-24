using Reservas.DataLayer;
using Reservas.DataLayer.Repositories;
using Reservas.ServiceLayer.Masks;
using Reservas.ServiceLayer.Requests;
using Reservas.ServiceLayer.Responses;

namespace Reservas.ServiceLayer.Services
{
    public interface IReservasService
    {
        CreateReservaResponse Create(CreateReservaRequest request);
        GetReservasResponse Get();
    }

    public class ReservasService : IReservasService
    {
        private IReservasRepository _repository;
        private IServiciosRepository _serviciosRepository;

        public ReservasService(IReservasRepository repository, IServiciosRepository serviciosRepository) 
        {
            _repository = repository;
            _serviciosRepository = serviciosRepository;
        }

        public CreateReservaResponse Create(CreateReservaRequest request)
        {
            // Turnos Disponibles de 9hs a 18hs

            var hsRange = Enumerable.Range(9, 17).ToList();

            if (!_serviciosRepository.Exists(request.Servicio))
                throw new Exception("El servicio solicitado no existe");

            if (!hsRange.Contains(request.Fecha.Hour))
                throw new Exception("El horario no es válido");

            if (_repository.Exists(request.Fecha))
                throw new Exception("Ya existe una reserva para ese día y horario");

            if (_repository.Exists(request.Fecha, request.Cliente))
                throw new Exception("El cliente ya tiene una reserva para ese día");

            var reserva = _repository.Create(request.Servicio, request.Fecha, request.Cliente);

            var res = new CreateReservaResponse();
            res.Reserva = new Masks.Reserva();
            res.Reserva.Fecha = request.Fecha;
            res.Reserva.Cliente = request.Cliente;
            res.Reserva.Servicio = request.Servicio;
            return res;
        }

        public GetReservasResponse Get()
        {
            var res = new GetReservasResponse();

            var reservas = _repository.GetAll();

            res.Reservas = reservas.Select(x => new Masks.Reserva()
            {
                Cliente = x.Cliente,
                Fecha = x.Fecha,
                Servicio = x.Servicio
            });

            return res;
        }
    }
}
