using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class CargoConfiguration : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.ToTable("cargo");

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(20);
            
            builder.Property(e => e.SueldoBase)
                .IsRequired()
                .HasColumnType("double");

            builder.HasData(
                new Cargo
                {
                    Id = 1,
                    Descripcion = "Auxiliar de Bodega",
                    SueldoBase = 5000000,
                },
                new Cargo
                {
                    Id = 2,
                    Descripcion = "Jefe de Producción",
                    SueldoBase = 6000000,
                },
                new Cargo
                {
                    Id = 3,
                    Descripcion = "Corte",
                    SueldoBase = 6000000,
                },
                new Cargo
                {
                    Id = 4,
                    Descripcion = "Jefe de bodega",
                    SueldoBase = 6000000,
                },
                new Cargo
                {
                    Id = 5,
                    Descripcion = "Secretaria",
                    SueldoBase = 6000000,
                },
                new Cargo
                {
                    Id = 6,
                    Descripcion = "Jefe de IT",
                    SueldoBase = 6000000,
                }
            );
        }
    }
}