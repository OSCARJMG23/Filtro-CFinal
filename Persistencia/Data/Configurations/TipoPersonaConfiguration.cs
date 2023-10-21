using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class TipoPersonaConfiguration : IEntityTypeConfiguration<TipoPersona>
    {
        public void Configure(EntityTypeBuilder<TipoPersona> builder)
        {
            builder.ToTable("tipoPersona");

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasData(
                new TipoPersona
                {
                    Id = 1,
                    Descripcion = "Juridica"
                },
                new TipoPersona
                {
                    Id = 2,
                    Descripcion = "Natural"
                }
            );
        }
    }
}