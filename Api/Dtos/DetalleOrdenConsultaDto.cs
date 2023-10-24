using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class DetalleOrdenConsultaDto
    {
        public string NombrePrenda { get; set; }
        public int CodPrenda { get; set; }
        public int Cantidad { get; set; }
        public double ValorCop { get; set; }
        public double ValorUsd { get; set; }
    }
}