using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reservas.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservas.DataLayer
{
    public static class DataLayerExtensions
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services) 
        {
            services.AddDbContext<Reservas.DataLayer.ReservasDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "ReservasDb");
            });

            services.AddScoped<IReservasRepository,ReservasRepository>();

            return services;
        }
    }
}
