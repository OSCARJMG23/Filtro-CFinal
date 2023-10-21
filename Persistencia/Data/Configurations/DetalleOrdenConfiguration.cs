using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class DetalleOrdenConfiguration : IEntityTypeConfiguration<DetalleOrden>
    {
        public void Configure(EntityTypeBuilder<DetalleOrden> builder)
        {
            builder.ToTable("detalleorden");

            builder.Property(e => e.CantidadProducir)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(e => e.CantidadProducida)
                .IsRequired()
                .HasColumnType("int");

            builder.HasOne(e => e.Orden)
                .WithMany(e => e.DetalleOrdenes)
                .HasForeignKey(e => e.IdOrdenFk);

            builder.HasOne(e => e.Prenda)
                .WithMany(e => e.DetalleOrdenes)
                .HasForeignKey(e => e.IdPrendaFk);

            builder.HasOne(e => e.Color)
                .WithMany(e => e.DetalleOrdenes)
                .HasForeignKey(e => e.IdColorFk);
            
            builder.HasOne(e => e.Estado)
                .WithMany(e => e.DetalleOrdenes)
                .HasForeignKey(e => e.IdEstadoFk);

            builder.HasData(
                new DetalleOrden
                {
                    Id = 1,
                    IdOrdenFk = 1,
                    IdPrendaFk = 1,
                    CantidadProducir = 5,
                    IdColorFk = 2,
                    CantidadProducida = 8,
                    IdEstadoFk = 1
                },
                new DetalleOrden
                {
                    Id = 2,
                    IdOrdenFk = 2,
                    IdPrendaFk = 2,
                    CantidadProducir = 4,
                    IdColorFk = 3,
                    CantidadProducida = 9,
                    IdEstadoFk = 2
                }
            );
        }
    }
}