using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class InsumoConfiguration : IEntityTypeConfiguration<Insumo>
    {
        public void Configure(EntityTypeBuilder<Insumo> builder)
        {
            builder.ToTable("insumo");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.ValorUnit)
                .IsRequired()
                .HasColumnType("double");

            builder.Property(e => e.StockMin)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(e => e.StockMax)
                .IsRequired()
                .HasColumnType("int");

            builder.HasMany(e => e.Prendas)
                .WithMany(r => r.Insumos)
                .UsingEntity<InsumoPrenda>(
                    j =>
                    {
                        j.HasOne(p => p.Prenda)
                         .WithMany(p => p.InsumoPrendas)
                         .HasForeignKey(p => p.IdPrendaFk);
            
                        j.HasOne(e => e.Insumo)
                         .WithMany(e => e.InsumosPrendas)
                         .HasForeignKey(e => e.IdInsumoFk);
            
                        j.ToTable("insumoPrenda");
                        j.HasKey(t => new { t.IdPrendaFk, t.IdInsumoFk });
                    });

                builder.HasIndex(t=>t.Nombre)
                .IsUnique();

            builder.HasMany(e => e.Proveedores)
                .WithMany(r => r.Insumos)
                .UsingEntity<InsumoProveedor>(
                    j =>
                    {
                        j.HasOne(p => p.Proveedor)
                         .WithMany(p => p.InsumoProveedores)
                         .HasForeignKey(p => p.IdProveedorFk);
            
                        j.HasOne(e => e.Insumo)
                         .WithMany(e => e.InsumosProveedores)
                         .HasForeignKey(e => e.IdInsumoFk);
            
                        j.ToTable("insumoProveedor");
                        j.HasKey(t => new { t.IdInsumoFk, t.IdProveedorFk });
                    });

                builder.HasData(
                    new Insumo
                    {
                        Id = 1,
                        Nombre = "Tela",
                        ValorUnit = 25000,
                        StockMin = 5,
                        StockMax = 50
                    },
                    new Insumo
                    {
                        Id = 2,
                        Nombre = "Hilos",
                        ValorUnit = 20000,
                        StockMin = 2,
                        StockMax = 42
                    }
                );
        }
    }
}