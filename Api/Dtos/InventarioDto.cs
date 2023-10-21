using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class InventarioDto
    {
        public int CodInventario { get; set; }
        public int IdPrendaFk { get; set; }
        public double ValorVentaCop { get; set; }
        public double ValorVentaUsd { get; set; }
    }
}