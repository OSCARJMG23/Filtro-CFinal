using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Api.Dtos
{
    public class OrdenXclienteDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdClienteFk { get; set; }
        public string NombreCliente { get; set; }
        public string Municipio { get; set; }
        public int IdEstadoFk { get; set; }
        public string Estado { get; set; }
        public List<DetalleOrdenConsultaDto> DetalleOrdenes { get; set; }
    }
}