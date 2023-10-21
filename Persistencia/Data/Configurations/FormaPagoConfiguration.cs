using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class FormaPagoConfiguration : IEntityTypeConfiguration<FormaPago>
    {
        public void Configure(EntityTypeBuilder<FormaPago> builder)
        {
            builder.ToTable("formaPago");

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(20);
            
            builder.HasData(
                new FormaPago
                {
                    Id = 1,
                    Descripcion = "Efectivo"
                },
                new FormaPago
                {
                    Id = 2,
                    Descripcion = "Tarjeta"
                }
            );
        }
    }
}