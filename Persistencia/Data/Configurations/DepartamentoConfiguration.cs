using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.ToTable("departamento");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(e => e.Pais)
                .WithMany(e => e.Departamentos)
                .HasForeignKey(e => e.IdPaisFk);

            builder.HasData(
                new Departamento
                {
                    Id = 1,
                    Nombre = "Santander",
                    IdPaisFk = 1
                },
                new Departamento
                {
                    Id = 2,
                    Nombre = "Cundinamarca",
                    IdPaisFk = 1
                }
            );
        }
    }
}