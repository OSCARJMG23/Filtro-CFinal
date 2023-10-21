using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("estado");

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(e => e.TipoEstado)
                .WithMany(e => e.Estados)
                .HasForeignKey(e => e.IdTipoEstadoFk);

            builder.HasData(
                new Estado
                {
                    Id = 1,
                    Descripcion = "Proceso",
                    IdTipoEstadoFk = 1
                },
                new Estado
                {
                    Id = 2,
                    Descripcion = "Terminado",
                    IdTipoEstadoFk = 2
                }
            );
        }
    }
}