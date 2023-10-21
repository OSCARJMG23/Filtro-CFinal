using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class DetalleVentaDto
    {
        public int IdVentaFk { get; set; }
        public int IdProductoFk { get; set; }
        public int IdTallaFk { get; set; }
        public int Cantidad { get; set; }
        public int ValorUnit { get; set; }
    }
}