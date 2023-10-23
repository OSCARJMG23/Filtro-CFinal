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
    public class OrdenRepository : GenericRepository<Orden>, IOrdenRepository
    {
        private readonly ApiContext _contex;
        public OrdenRepository(ApiContext context) : base(context)
        {
            _contex = context;
        }
        public override async Task<(int totalRegistros, IEnumerable<Orden> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _contex.Ordenes as IQueryable<Orden>;
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

        public async Task<IEnumerable<Orden>> OrdenesEnProceso()
        {
            var orden = await _contex.Ordenes
            .Where(t=>t.Estado.Descripcion == "Proceso")
            .Include(e=>e.Empleado)
            .Include(e=>e.Cliente)
            .Include(e=>e.Estado)
            .ToListAsync();

            return orden;
        }
        public async Task<IEnumerable<Orden>> OrdenXCliente(int clienteConsulta)
        {
            var orden = await _contex.Ordenes
            .Where(t=>t.Cliente.IdCliente == clienteConsulta)
            .ToListAsync();

            return orden;
        }
    }
}