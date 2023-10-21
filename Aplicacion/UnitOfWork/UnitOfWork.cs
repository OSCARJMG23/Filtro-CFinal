using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApiContext _context;

        public UnitOfWork(ApiContext context)
        {
            _context = context;
        }
        private IRolRepository _roles;
        private IUserRepository _users;
        private ICargoRepository _cargos ;
        private IClienteRepository _clientes ;
        private IColorRepository _Colores ;
        private IDepartamentoRepository _Departamentos ;
        private IDetalleOrdenRepository _DetalleOrdenes ;
        private IDetalleVentaRepository _DetalleVentas ;
        private IEmpleadoRepository _Empleados ;
        private IEmpresaRepository _Empresas ;
        private IEstadoRepository _Estados ;
        private IFormaPagoRepository _FormaPagos ;
        private IGeneroRepository _Generos ;
        private IInsumoRepository _Insumos ;
        private IInventarioRepository _Inventarios ;
        private IMunicipioRepository _Municipios ;
        private IOrdenRepository _Ordenes ;
        private IPaisRepository _Paises ;
        private IPrendaRepository _Prendas ;
        private IProveedorRepository _Proveedores ;
        private ITallaRepository _Tallas ;
        private ITipoEstadoRepository _TipoEstados ;
        private ITipoPersonaRepository _TipoPersonas ;
        private ITipoProteccionRepository _TipoProtecciones ;
        private IVentaRepository _Ventas ;

        public IRolRepository Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new RolRepository(_context);
                }
                return _roles;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRepository(_context);
                }
                return _users;
            }
        }
        public ICargoRepository Cargos
        {
            get
            {
                if (_cargos == null)
                {
                    _cargos = new CargoRepository(_context);
                }
                return _cargos;
            }
        }
        public IClienteRepository Clientes
        {
            get
            {
                if (_clientes == null)
                {
                    _clientes = new ClienteRepository(_context);
                }
                return _clientes;
            }
        }
        public IColorRepository Colores
        {
            get
            {
                if (_Colores == null)
                {
                    _Colores = new ColorRepository(_context);
                }
                return _Colores;
            }
        }
        public IDepartamentoRepository Departamentos
        {
            get
            {
                if (_Departamentos == null)
                {
                    _Departamentos = new DepartamentoRepository(_context);
                }
                return _Departamentos;
            }
        }
        public IDetalleOrdenRepository DetalleOrdenes
        {
            get
            {
                if (_DetalleOrdenes == null)
                {
                    _DetalleOrdenes = new DetalleOrdenRepository(_context);
                }
                return _DetalleOrdenes;
            }
        }
        public IDetalleVentaRepository DetalleVentas
        {
            get
            {
                if (_DetalleVentas == null)
                {
                    _DetalleVentas = new DetalleVentaRepository(_context);
                }
                return _DetalleVentas;
            }
        }
        public IEmpleadoRepository Empleados
        {
            get
            {
                if (_Empleados == null)
                {
                    _Empleados = new EmpleadoRepository(_context);
                }
                return _Empleados;
            }
        }
        public IEmpresaRepository Empresas
        {
            get
            {
                if (_Empresas == null)
                {
                    _Empresas = new EmpresaRepository(_context);
                }
                return _Empresas;
            }
        }
        public IEstadoRepository Estados
        {
            get
            {
                if (_Estados == null)
                {
                    _Estados = new EstadoRepository(_context);
                }
                return _Estados;
            }
        }
        public IFormaPagoRepository FormaPagos
        {
            get
            {
                if (_FormaPagos == null)
                {
                    _FormaPagos = new FormaPagoRepository(_context);
                }
                return _FormaPagos;
            }
        }
        public IGeneroRepository Generos
        {
            get
            {
                if (_Generos == null)
                {
                    _Generos = new GeneroRepository(_context);
                }
                return _Generos;
            }
        }
        public IInsumoRepository Insumos
        {
            get
            {
                if (_Insumos == null)
                {
                    _Insumos = new InsumoRepository(_context);
                }
                return _Insumos;
            }
        }
        public IInventarioRepository Inventarios
        {
            get
            {
                if (_Inventarios == null)
                {
                    _Inventarios = new InventarioRepository(_context);
                }
                return _Inventarios;
            }
        }
        public IMunicipioRepository Municipios
        {
            get
            {
                if (_Municipios == null)
                {
                    _Municipios = new MunicipioRepository(_context);
                }
                return _Municipios;
            }
        }
        public IOrdenRepository Ordenes
        {
            get
            {
                if (_Ordenes == null)
                {
                    _Ordenes = new OrdenRepository(_context);
                }
                return _Ordenes;
            }
        }
        public IPaisRepository Paises
        {
            get
            {
                if (_Paises == null)
                {
                    _Paises = new PaisRepository(_context);
                }
                return _Paises;
            }
        }
        public IPrendaRepository Prendas
        {
            get
            {
                if (_Prendas == null)
                {
                    _Prendas = new PrendaRepository(_context);
                }
                return _Prendas;
            }
        }
        public IProveedorRepository Proveedores
        {
            get
            {
                if (_Proveedores == null)
                {
                    _Proveedores = new ProveedorRepository(_context);
                }
                return _Proveedores;
            }
        }
        public ITallaRepository Tallas
        {
            get
            {
                if (_Tallas == null)
                {
                    _Tallas = new TallaRepository(_context);
                }
                return _Tallas;
            }
        }
        public ITipoEstadoRepository TipoEstados
        {
            get
            {
                if (_TipoEstados == null)
                {
                    _TipoEstados = new TipoEstadoRepository(_context);
                }
                return _TipoEstados;
            }
        }
        public ITipoPersonaRepository TipoPersonas
        {
            get
            {
                if (_TipoPersonas == null)
                {
                    _TipoPersonas = new TipoPersonaRepository(_context);
                }
                return _TipoPersonas;
            }
        }
        public ITipoProteccionRepository TipoProtecciones
        {
            get
            {
                if (_TipoProtecciones == null)
                {
                    _TipoProtecciones = new TipoProteccionRepository(_context);
                }
                return _TipoProtecciones;
            }
        }
        public IVentaRepository Ventas
        {
            get
            {
                if (_Ventas == null)
                {
                    _Ventas = new VentaRepository(_context);
                }
                return _Ventas;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}