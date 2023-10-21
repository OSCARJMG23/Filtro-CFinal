using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class EmpleadoDto
    {
        public double IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public int IdCargoFk { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int IdMunicipioFk { get; set; }
        
    }
}