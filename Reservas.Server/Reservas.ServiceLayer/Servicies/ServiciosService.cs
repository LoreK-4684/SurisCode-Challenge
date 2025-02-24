
using Reservas.DataLayer.Repositories;
using Reservas.ServiceLayer.Masks;
using Reservas.ServiceLayer.Requests;
using Reservas.ServiceLayer.Responses;

namespace Reservas.ServiceLayer.Services
{
    public interface IServiciosService
    {
        GetServiciosResponse Get();
    }

    public class ServiciosService : IServiciosService
    {
        private IServiciosRepository _repository;

        public ServiciosService(IServiciosRepository repository) 
        {
            _repository = repository;
        }

        public GetServiciosResponse Get()
        {
            var res = new GetServiciosResponse();

            res.Servicios = _repository.GetAll().Select(x => new Servicio() { Name = x }).ToArray();

            return res;
        }
    }
}
