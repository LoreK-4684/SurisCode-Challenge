using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservas.DataLayer.Entities;

namespace Reservas.DataLayer.EntitiesConfigurations
{
    public class Reserva : IEntityTypeConfiguration<Entities.Reserva>
    {
        public void Configure(EntityTypeBuilder<Entities.Reserva> builder)
        {
            builder.HasKey(x => x.Id);
        }

    }
}