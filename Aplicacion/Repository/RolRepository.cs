using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class RolRepository : GenericRepository<Rol>, IRolRepository
    {
        private readonly ApiContext _contex;

        public RolRepository(ApiContext context) : base(context)
        {
            _contex = context;
        }
    }
}