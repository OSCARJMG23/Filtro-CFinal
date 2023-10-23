using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class OrdenProcesoDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEmpleadoFk { get; set; }
        public string NombreEmpleado { get; set; }
        public int IdClienteFk { get; set; }
        public string NombreCliente { get; set; }
        public int IdEstadoFk { get; set; }
        public string Estado { get; set; }
    }
}