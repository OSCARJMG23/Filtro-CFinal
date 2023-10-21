using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable("Color");
            
            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasData(
                new Color
                {
                    Id = 1,
                    Descripcion = "Blanco"
                },
                new Color
                {
                    Id = 2,
                    Descripcion = "Negro"
                },
                new Color
                {
                    Id = 3,
                    Descripcion = "Gris"
                }
            );
            
        }
    }
}