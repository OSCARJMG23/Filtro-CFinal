using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class OrdenConfiguration : IEntityTypeConfiguration<Orden>
    {
        public void Configure(EntityTypeBuilder<Orden> builder)
        {
            builder.ToTable("orden");

            builder.HasOne(e => e.Empleado)
                .WithMany(e => e.Ordenes)
                .HasForeignKey(e => e.IdEmpleadoFk);

            builder.HasOne(e => e.Cliente)
                .WithMany(e => e.Ordenes)
                .HasForeignKey(e => e.IdClienteFk);

            builder.HasOne(e => e.Estado)
                .WithMany(e => e.Ordenes)
                .HasForeignKey(e => e.IdEstadoFk);

            builder.HasData(
                new Orden
                {
                    Id = 1,
                    Fecha = new DateTime(2023, 11, 20),
                    IdEmpleadoFk = 1,
                    IdClienteFk = 1,
                    IdEstadoFk =1
                },
                new Orden
                {
                    Id = 2,
                    Fecha = new DateTime(2023, 11, 20),
                    IdEmpleadoFk = 2,
                    IdClienteFk = 2,
                    IdEstadoFk =2
                }
            );
        }
    }
}