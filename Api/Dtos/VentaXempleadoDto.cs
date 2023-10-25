using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class VentaXempleadoDto
    {
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public double Total { get; set; }

    }
}