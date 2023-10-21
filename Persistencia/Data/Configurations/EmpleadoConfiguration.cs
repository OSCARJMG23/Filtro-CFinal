using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.ToTable("Empleado");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.IdEmpleado)
                .IsRequired()
                .HasColumnType("double");

            builder.HasOne(e => e.Municipio)
                .WithMany(e => e.Empleados)
                .HasForeignKey(e => e.IdMunicipioFk);
            
            builder.HasOne(e => e.Cargo)
                .WithMany(e => e.Empleados)
                .HasForeignKey(e => e.IdCargoFk);

            builder.HasIndex(t=>t.IdEmpleado)
                .IsUnique();

            builder.HasData(
                new Empleado
                {
                    Id = 1,
                    IdEmpleado = 1234,
                    Nombre = "Pablo Gomez",
                    IdCargoFk = 1,
                    FechaIngreso = new DateTime(2023, 11, 20),
                    IdMunicipioFk = 1
                },
                new Empleado
                {
                    Id = 2,
                    IdEmpleado = 12345,
                    Nombre = "Pablo Gonzalez",
                    IdCargoFk = 2,
                    FechaIngreso = new DateTime(2023, 11, 20),
                    IdMunicipioFk = 2
                }
            );
        }
    }
}