using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class PaisConfiguration: IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("pais");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasData(
                new Pais
                {
                    Id = 1,
                    Nombre = "Colombia"
                },
                new Pais
                {
                    Id = 2,
                    Nombre = "Venezuela"
                }
            );
        }
    }
}