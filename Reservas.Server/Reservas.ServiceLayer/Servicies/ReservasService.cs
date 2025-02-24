using Reservas.DataLayer;
using Reservas.DataLayer.Repositories;
using Reservas.ServiceLayer.Masks;
using Reservas.ServiceLayer.Requests;
using Reservas.ServiceLayer.Responses;
using Reservas.ServiceLayer.Support;

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

            var hsRange = Enumerable.Range(9, 9).ToList();

            var dt = DateTime.Parse(request.Fecha);

            if (!_serviciosRepository.Exists(request.Servicio))
                throw new BusinessException("El servicio solicitado no existe");

            if (!hsRange.Contains(dt.Hour))
                throw new BusinessException("El horario no es válido");

            if (_repository.Exists(dt))
                throw new BusinessException("Ya existe una reserva para ese día y horario");

            if (_repository.Exists(dt, request.Cliente))
                throw new BusinessException("El cliente ya tiene una reserva para ese día");

            var reserva = _repository.Create(request.Servicio, dt, request.Cliente);

            var res = new CreateReservaResponse();
            res.Reserva = new Masks.Reserva();
            res.Reserva.Id = reserva.Id;
            res.Reserva.Fecha = reserva.Fecha;
            res.Reserva.Cliente = reserva.Cliente;
            res.Reserva.Servicio = reserva.Servicio;
            return res;
        }

        public GetReservasResponse Get()
        {
            var res = new GetReservasResponse();

            var reservas = _repository.GetAll();

            res.Reservas = reservas.Select(x => new Masks.Reserva()
            {
                Id = x.Id,
                Cliente = x.Cliente,
                Fecha = x.Fecha,
                Servicio = x.Servicio
            });

            return res;
        }
    }
}
