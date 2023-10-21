using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class DetalleVentaConfiguration : IEntityTypeConfiguration<DetalleVenta>
    {
        public void Configure(EntityTypeBuilder<DetalleVenta> builder)
        {
            builder.ToTable("detalleventa");

            builder.Property(e => e.Cantidad)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(e => e.ValorUnit)
                .IsRequired()
                .HasColumnType("int");

            builder.HasOne(e => e.Venta)
                .WithMany(e => e.DetalleVentas)
                .HasForeignKey(e => e.IdVentaFk);

            builder.HasOne(e => e.Talla)
                .WithMany(e => e.DetalleVentas)
                .HasForeignKey(e => e.IdTallaFk);

            builder.HasOne(e => e.Inventario)
                .WithMany(e => e.DetallesVentas)
                .HasForeignKey(e => e.IdProductoFk);

            builder.HasData(
                new DetalleVenta
                {
                    Id = 1,
                    IdVentaFk = 1,
                    IdProductoFk = 1,
                    IdTallaFk = 1,
                    Cantidad = 3,
                    ValorUnit = 25000
                },
                new DetalleVenta
                {
                    Id = 2,
                    IdVentaFk = 1,
                    IdProductoFk = 1,
                    IdTallaFk = 1,
                    Cantidad = 3,
                    ValorUnit = 25000
                }
            );
        }
    }
}