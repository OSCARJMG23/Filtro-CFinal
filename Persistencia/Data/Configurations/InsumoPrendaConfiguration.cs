using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class InsumoPrendaConfiguration : IEntityTypeConfiguration<InsumoPrenda>
    {
        public void Configure(EntityTypeBuilder<InsumoPrenda> builder)
        {
            builder.ToTable("insumoPrenda");
            builder.HasData(
                new InsumoPrenda
                {
                    IdInsumoFk = 1,
                    IdPrendaFk=2
                },
                new InsumoPrenda
                {
                    IdInsumoFk = 2,
                    IdPrendaFk = 2
                }
            );
        }
    }
}