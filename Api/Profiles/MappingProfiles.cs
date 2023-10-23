using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Dominio.Entities;

namespace Api.Profiles
{
    public class MappingProfiles : Profile
    {
        /* CreateMap<Especie, EspecieDto>().ReverseMap(); */
        public MappingProfiles()
        {
            
            CreateMap<Cargo , CargoDto>().ReverseMap();
            CreateMap<Cliente , ClienteDto>().ReverseMap();
            CreateMap<Color , ColorDto>().ReverseMap();
            CreateMap<Departamento , DepartamentoDto>().ReverseMap();
            CreateMap<DetalleOrden , DetalleOrdenDto>().ReverseMap();
            CreateMap<DetalleOrden , DetalleOrdenDto>().ReverseMap();
            CreateMap<DetalleVenta , DetalleVentaDto>().ReverseMap();
            CreateMap<Empleado , EmpleadoDto>().ReverseMap();
            CreateMap<Empresa , EmpresaDto>().ReverseMap();
            CreateMap<Estado , EstadoDto>().ReverseMap();
            CreateMap<FormaPago , FormaPagoDto>().ReverseMap();
            CreateMap<Genero , GeneroDto>().ReverseMap();
            CreateMap<Insumo , InsumoDto>().ReverseMap();
            CreateMap<Inventario , InventarioDto>().ReverseMap();
            CreateMap<Municipio , MunicipioDto>().ReverseMap();
            CreateMap<Orden , OrdenDto>().ReverseMap();
            CreateMap<Orden , OrdenProcesoDto>()
            .ForMember(t=>t.IdEmpleadoFk, e => e.MapFrom(f => f.Empleado.Id))
            .ForMember(t=>t.NombreEmpleado, e=> e.MapFrom(t=>t.Empleado.Nombre))
            .ForMember(t=> t.IdClienteFk, e=> e.MapFrom(t=>t.Cliente.Id))
            .ForMember(t=>t.NombreCliente, e=>e.MapFrom(t=>t.Cliente.Nombre))
            .ForMember(t=> t.IdEstadoFk, e=>e.MapFrom(t=>t.Estado.Id))
            .ForMember(t=>t.Estado, e=>e.MapFrom(t=>t.Estado.Descripcion))
            .ReverseMap();



            CreateMap<Pais , PaisDto>().ReverseMap();
            CreateMap<Prenda , PrendaDto>().ReverseMap();
            CreateMap<Proveedor , ProveedorDto>().ReverseMap();
            CreateMap<Talla , TallaDto>().ReverseMap();
            CreateMap<TipoEstado , TipoEstadoDto>().ReverseMap();
            CreateMap<TipoProteccion , TipoProteccionDto>().ReverseMap();
            CreateMap<TipoPersona , TipoPersonaDto>().ReverseMap();
            CreateMap<Venta , VentaDto>().ReverseMap();
        }
    }
}