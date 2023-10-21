using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IInsumoRepository : IGenericRepository<Insumo>
    {
        Task<IEnumerable<Insumo>> InsumosXPrenda(int codigoPrenda);
        Task<IEnumerable<Insumo>> InsumosXProveedor(int nitProveedorConsulta);
    }
}