using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class PrendaDto
    {
        public int IdPrenda { get; set; }
        public string Nombre { get; set; }
        public double ValorUnitCop { get; set; }
        public double ValorUnitUsd { get; set; }
        public int IdEstadoFk { get; set; }
        
        public int IdTipoProteccionFk { get; set; }
        
        public int IdGeneroFk { get; set; }
        
    }
}