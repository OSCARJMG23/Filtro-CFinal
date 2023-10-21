using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class GeneroConfiguration : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.ToTable("Genero");

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasData(
                new Genero
                {
                    Id = 1,
                    Descripcion = "Masculino"
                },
                new Genero
                {
                    Id = 2,
                    Descripcion = "Femenino"
                }
            );
        }
    }
}