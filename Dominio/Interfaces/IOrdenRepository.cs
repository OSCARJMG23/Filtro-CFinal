using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IOrdenRepository : IGenericRepository<Orden>
    {
        Task<IEnumerable<Orden>> OrdenesEnProceso();
        Task<IEnumerable<Orden>> OrdenXCliente(int clienteConsulta);
    }
}