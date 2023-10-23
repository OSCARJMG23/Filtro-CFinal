using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class InsumoRepository : GenericRepository<Insumo>, IInsumoRepository
    {
        private readonly ApiContext _contex;
        public InsumoRepository(ApiContext context) : base(context)
        {
                _contex = context;
        }
        public override async Task<(int totalRegistros, IEnumerable<Insumo> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _contex.Insumos as IQueryable<Insumo>;
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Nombre.ToLower().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                                /* .Include(u => u.Proveedor) */
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
            return (totalRegistros, registros);
        }
        public async Task<IEnumerable<Insumo>> InsumosXPrenda(int codigoPrenda)
        {
            var insumo = await _contex.Insumos
            .Where(t=> t.Prendas.Any(t=>t.IdPrenda == codigoPrenda))
            .ToListAsync();
            return insumo;
        }

        public async Task<IEnumerable<Insumo>> InsumosXProveedor(int nitProveedorConsulta)
        {
            var insumo = await _contex.Insumos
            .Where(t=>t.Proveedores.Any(t=>t.NitProveedor == nitProveedorConsulta && t.TipoPersona.Descripcion == "Juridica"))
            .ToListAsync();

            return insumo;
        }
    }
}