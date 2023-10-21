using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("cliente");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.IdCliente)
                .IsRequired()
                .HasColumnType("double");

            builder.HasOne(e => e.Municipio)
                .WithMany(e => e.Clientes)
                .HasForeignKey(e => e.IdMunicipioFk);

            builder.HasOne(e => e.TipoPersona)
                .WithMany(e => e.Clientes)
                .HasForeignKey(e => e.IdTipoPersonaFk);

            builder.HasIndex(t=>t.IdCliente)
                .IsUnique();

            builder.HasData(
                new Cliente
                {
                    Id = 1,
                    IdCliente = 12345,
                    Nombre = "Andres Gomez",
                    IdTipoPersonaFk = 1,
                    FechaRegistro = new DateTime(2023, 11, 20),
                    IdMunicipioFk = 1
                },
                new Cliente
                {
                    Id = 2,
                    IdCliente = 123456,
                    Nombre = "Marta Angeles",
                    IdTipoPersonaFk = 1,
                    FechaRegistro = new DateTime(2023, 11, 20),
                    IdMunicipioFk = 2
                },
                new Cliente
                {
                    Id = 3,
                    IdCliente = 1234567,
                    Nombre = "Camilo Lopera",
                    IdTipoPersonaFk = 2,
                    FechaRegistro = new DateTime(2023, 11, 20),
                    IdMunicipioFk = 2
                }
            );
        }
    }
}