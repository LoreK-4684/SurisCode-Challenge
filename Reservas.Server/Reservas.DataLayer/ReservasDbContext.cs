using Microsoft.EntityFrameworkCore;
using Reservas.DataLayer.Entities;
using Reservas.DataLayer.EntitiesConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservas.DataLayer
{
    public class ReservasDbContext : DbContext
    {
        public ReservasDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EntitiesConfigurations.Reserva());
        }
    }
}
