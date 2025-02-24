using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reservas.DataLayer.Repositories;
using Reservas.ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservas.ServiceLayer
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services) 
        {
            services.AddScoped<IReservasService, ReservasService>();
            services.AddScoped<ITurnosService, TurnosService>();
            services.AddScoped<IServiciosService, ServiciosService>();

            return services;
        }
    }
}
