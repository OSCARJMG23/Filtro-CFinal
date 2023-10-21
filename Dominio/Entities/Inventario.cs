using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Inventario : BaseEntity
    {
        public int CodInventario { get; set; }
        public int IdPrendaFk { get; set; }
        public Prenda Prenda { get; set; }
        public double ValorVentaCop { get; set; }
        public double ValorVentaUsd { get; set; }
        public ICollection<DetalleVenta> DetallesVentas { get; set; }
        public ICollection<InventarioTalla> InventarioTallas { get; set; }
        public ICollection<Talla> Tallas { get; set; }
    }
}