using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class InventarioTallaConfiguration : IEntityTypeConfiguration<InventarioTalla>
    {
        public void Configure(EntityTypeBuilder<InventarioTalla> builder)
        {
            builder.ToTable("inventariotalla");
            builder.HasData(
                new InventarioTalla
                {
                    IdInventarioFk = 1,
                    IdTallaFk = 1
                },
                new InventarioTalla
                {
                    IdInventarioFk = 2,
                    IdTallaFk = 2
                }
            );
        }
    }
}