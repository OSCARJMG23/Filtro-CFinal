using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class DetalleOrden : BaseEntity
    {
        public int IdOrdenFk { get; set; }
        public Orden Orden { get; set; }
        public int IdPrendaFk { get; set; }
        public Prenda Prenda { get; set; }
        public int CantidadProducir { get; set; }
        public int IdColorFk { get; set; }
        public Color Color { get; set; }
        public int CantidadProducida { get; set; }
        public int IdEstadoFk { get; set; }
        public Estado Estado { get; set; }
    }
}