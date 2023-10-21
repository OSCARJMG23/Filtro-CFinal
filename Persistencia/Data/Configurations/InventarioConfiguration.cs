using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class InventarioConfiguration : IEntityTypeConfiguration<Inventario>
    {
        public void Configure(EntityTypeBuilder<Inventario> builder)
        {
            builder.ToTable("Inventario");

            builder.Property(e => e.CodInventario)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(e => e.ValorVentaCop)
                .IsRequired()
                .HasColumnType("double");
            
            builder.Property(e => e.ValorVentaUsd)
                .IsRequired()
                .HasColumnType("int");
            
            builder.HasMany(e => e.Tallas)
                .WithMany(r => r.Inventarios)
                .UsingEntity<InventarioTalla>(
                    j =>
                    {
                        j.HasOne(p => p.Inventario)
                         .WithMany(p => p.InventarioTallas)
                         .HasForeignKey(p => p.IdInventarioFk);
            
                        j.HasOne(e => e.Talla)
                         .WithMany(e => e.InventarioTallas)
                         .HasForeignKey(e => e.IdTallaFk);
            
                        j.ToTable("inventarioTalla");
                        j.HasKey(t => new { t.IdInventarioFk, t.IdTallaFk });
                    });

            builder.HasIndex(t=>t.CodInventario)
                .IsUnique();

            builder.HasData(
                new Inventario
                {
                    Id = 1,
                    CodInventario=124,
                    IdPrendaFk = 1,
                    ValorVentaCop =35000,
                    ValorVentaUsd=25
                },
                new Inventario
                {
                    Id = 2,
                    CodInventario=1234,
                    IdPrendaFk = 1,
                    ValorVentaCop =45000,
                    ValorVentaUsd=28
                }
            );
        }
    }
}