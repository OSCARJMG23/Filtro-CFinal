using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            builder.ToTable("proveedor");

            builder.Property(e => e.NitProveedor)
                .IsRequired()
                .HasColumnType("int");
            
            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(20);
            
            builder.HasOne(e => e.TipoPersona)
                .WithMany(e => e.Proveedores)
                .HasForeignKey(e => e.IdTipoPersonaFk);

            builder.HasOne(e => e.Municipio)
                .WithMany(e => e.Proveedores)
                .HasForeignKey(e => e.IdMunicipioFk);

            builder.HasIndex(t=>t.NitProveedor)
                .IsUnique();

            builder.HasData(
                new Proveedor
                {
                    Id = 1,
                    NitProveedor = 1234,
                    Nombre = "Juan Rocha",
                    IdTipoPersonaFk = 1,
                    IdMunicipioFk = 1
                },
                new Proveedor
                {
                    Id = 2,
                    NitProveedor = 2234,
                    Nombre = "Juan Rocha",
                    IdTipoPersonaFk = 2,
                    IdMunicipioFk = 2
                }
            );
        }
    }
}