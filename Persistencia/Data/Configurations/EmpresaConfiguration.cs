using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresa");

            builder.Property(e => e.Nit)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(e => e.RazonSocial)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(e => e.RepresentanteLegal)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.HasOne(e => e.Municipio)
                .WithMany(e => e.Empresas)
                .HasForeignKey(e => e.IdMunicipioFk);

            builder.HasIndex(t=>t.Nit)
                .IsUnique();

            builder.HasData(
                new Empresa
                {
                    Id = 1,
                    Nit = 1234,
                    RazonSocial ="Humanitaria",
                    RepresentanteLegal ="Marcos Nuñez",
                    FechaCreacion = new DateTime(2023, 11, 20),
                    IdMunicipioFk = 1
                },
                new Empresa
                {
                    Id = 2,
                    Nit = 12345,
                    RazonSocial ="Humanitaria",
                    RepresentanteLegal ="Camilo Nuñez",
                    FechaCreacion = new DateTime(2023, 11, 20),
                    IdMunicipioFk = 2
                }
            );
        }
    }
}