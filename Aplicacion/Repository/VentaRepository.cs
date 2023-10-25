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
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly ApiContext _contex;
        public VentaRepository(ApiContext context) : base(context)
        {
            _contex = context;
        }
        public override async Task<(int totalRegistros, IEnumerable<Venta> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _contex.Ventas as IQueryable<Venta>;
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Id.Equals(int.Parse(search)));
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
        public async Task<IEnumerable<Venta>> VentaXEmpleado(int IdEmpleadoConsulta)
        {
            var venta = await _contex.Ventas
            .Where(t=>t.IdEmpleadoFk == IdEmpleadoConsulta)
            .Include(t=>t.Empleado)
            .ThenInclude(t=>t.Ventas)
            .ThenInclude(t=>t.DetalleVentas)
            .ToListAsync();

            return venta;
        }
    }
}