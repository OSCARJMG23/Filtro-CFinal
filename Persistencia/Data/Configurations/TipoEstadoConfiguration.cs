using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class TipoEstadoConfiguration : IEntityTypeConfiguration<TipoEstado>
    {
        public void Configure(EntityTypeBuilder<TipoEstado> builder)
        {
            builder.ToTable("tipoEstado");

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(20);
            
            builder.HasData(
                new TipoEstado
                {
                    Id = 1,
                    Descripcion = "Fabricacion"
                },
                new TipoEstado
                {
                    Id = 2,
                    Descripcion = "Terminado"
                }
            );
        }
    }
}