using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class PrendaConfiguration : IEntityTypeConfiguration<Prenda>
    {
        public void Configure(EntityTypeBuilder<Prenda> builder)
        {
            builder.ToTable("prenda");

            builder.Property(e => e.IdPrenda)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(20);
            
            builder.Property(e => e.ValorUnitCop)
                .IsRequired()
                .HasColumnType("double");
            
            builder.Property(e => e.ValorUnitUsd)
                .IsRequired()
                .HasColumnType("double");

            builder.HasOne(e => e.Estado)
                .WithMany(e => e.Prendas)
                .HasForeignKey(e => e.IdEstadoFk);

            builder.HasOne(e => e.TipoProteccion)
                .WithMany(e => e.Prendas)
                .HasForeignKey(e => e.IdTipoProteccionFk);

            builder.HasOne(e => e.Genero)
                .WithMany(e => e.Prendas)
                .HasForeignKey(e => e.IdGeneroFk);

            builder.HasIndex(t=>t.IdPrenda)
                .IsUnique();

            builder.HasData(
                new Prenda
                {
                    Id = 1,
                    IdPrenda= 123,
                    Nombre = "Camiseta",
                    ValorUnitCop =35000,
                    ValorUnitUsd = 25,
                    IdEstadoFk =1,
                    IdTipoProteccionFk =1,
                    IdGeneroFk =1
                },
                new Prenda
                {
                    Id = 2,
                    IdPrenda= 223,
                    Nombre = "Camiseta",
                    ValorUnitCop =35000,
                    ValorUnitUsd = 25,
                    IdEstadoFk =2,
                    IdTipoProteccionFk =2,
                    IdGeneroFk =2
                }
            );
        }
    }
}