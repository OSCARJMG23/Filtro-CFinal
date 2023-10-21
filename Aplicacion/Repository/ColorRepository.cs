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
    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {
        private readonly ApiContext _contex;
        public ColorRepository(ApiContext context) : base(context)
        {
            _contex = context;
        }
        public override async Task<(int totalRegistros, IEnumerable<Color> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _contex.Colores as IQueryable<Color>;
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Descripcion.ToLower().Contains(search));
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
    }
}