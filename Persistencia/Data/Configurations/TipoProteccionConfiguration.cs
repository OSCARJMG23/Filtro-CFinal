using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class TipoProteccionConfiguration : IEntityTypeConfiguration<TipoProteccion>
    {
        public void Configure(EntityTypeBuilder<TipoProteccion> builder)
        {
            builder.ToTable("tipoProteccion");
            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasData(
                new TipoProteccion
                {
                    Id = 1,
                    Descripcion = "Brazos"
                },
                new TipoProteccion
                {
                    Id = 2,
                    Descripcion = "Cabeza"
                }
            );
        }
    }
}