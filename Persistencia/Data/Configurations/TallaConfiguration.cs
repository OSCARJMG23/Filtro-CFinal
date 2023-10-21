using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class TallaConfiguration : IEntityTypeConfiguration<Talla>
    {
        public void Configure(EntityTypeBuilder<Talla> builder)
        {
            builder.ToTable("talla");

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(t=>t.Descripcion)
                .IsUnique();

            builder.HasData(
                new Talla
                {
                    Id = 1,
                    Descripcion = "Xl"
                },
                new Talla
                {
                    Id = 2,
                    Descripcion = "Xs"
                }
            );
        }
    }
}