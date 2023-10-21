using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class VentaConfiguration : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.ToTable("venta");

            builder.HasOne(e => e.Empleado)
                .WithMany(e => e.Ventas)
                .HasForeignKey(e => e.IdEmpleadoFk);

            builder.HasOne(e => e.Cliente)
                .WithMany(e => e.Ventas)
                .HasForeignKey(e => e.IdClienteFk);

            builder.HasOne(e => e.FormaPago)
                .WithMany(e => e.Ventas)
                .HasForeignKey(e => e.IdFormaPagoFk);

            builder.HasData(
                new Venta
                {
                    Id = 1,
                    Fecha = new DateTime(2023, 11, 20),
                    IdEmpleadoFk =1,
                    IdClienteFk=1,
                    IdFormaPagoFk = 1
                },
                new Venta
                {
                    Id = 2,
                    Fecha = new DateTime(2023, 11, 20),
                    IdEmpleadoFk =2,
                    IdClienteFk=2,
                    IdFormaPagoFk = 2
                }
            );
        }
    }
}