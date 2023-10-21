using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class MunicipioConfiguration : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {
            builder.ToTable("municipio");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(e => e.Departamento)
                .WithMany(e => e.Municipios)
                .HasForeignKey(e => e.IdDepartamentoFk);

            builder.HasData(
                new Municipio
                {
                    Id = 1,
                    Nombre = "Bucaramanga",
                    IdDepartamentoFk = 1
                },
                new Municipio
                {
                    Id = 2,
                    Nombre = "Bogota",
                    IdDepartamentoFk = 2
                }
            );
        }
    }
}