using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class InsumoProveedorConfiguration : IEntityTypeConfiguration<InsumoProveedor>
    {
        public void Configure(EntityTypeBuilder<InsumoProveedor> builder)
        {
            builder.ToTable("insumoProveedor");
            builder.HasData(
                new InsumoProveedor
                {
                    IdInsumoFk = 1,
                    IdProveedorFk =1
                },
                new InsumoProveedor
                {
                    IdInsumoFk = 2,
                    IdProveedorFk  = 2
                }
            );
        }
    }
}