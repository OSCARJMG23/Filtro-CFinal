using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Authorize]
    public class EmpleadoController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public EmpleadoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<EmpleadoDto>>> Get([FromQuery]Params empleadpParams)
        {
            var movimiento = await _unitOfWork.Empleados.GetAllAsync(empleadpParams.PageIndex, empleadpParams.PageSize, empleadpParams.Search);
            var listaEmpleadosDto = _mapper.Map<List<EmpleadoDto>>(movimiento.registros);
            return new Pager<EmpleadoDto>(listaEmpleadosDto, movimiento.totalRegistros, empleadpParams.PageIndex, empleadpParams.PageSize, empleadpParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmpleadoDto>> Get(int id)
        {
            var detalle = await _unitOfWork.Empleados.GetByIdAsync(id);
            return _mapper.Map<EmpleadoDto>(detalle);
        }
        [HttpGet("cargo/{cargoConsulta}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmpleadoXcargoDto>>> GetEmpleadoPorCargo(string cargoConsulta)
        {
            var detalle = await _unitOfWork.Empleados.EmpleadosXCargo(cargoConsulta);
            return _mapper.Map<List<EmpleadoXcargoDto>>(detalle);
        }
        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Empleado>> Post(EmpleadoDto EmpleadoDto)
        {
            var empleado = _mapper.Map<Empleado>(EmpleadoDto);
            _unitOfWork.Empleados.Add(empleado);
            await _unitOfWork.SaveAsync();
        
            if (empleado == null)
            {
                return BadRequest();
            }
            empleado.Id = empleado.Id;
            return CreatedAtAction(nameof(Post), new { id = empleado.Id }, empleado);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmpleadoDto>> Put(int id, [FromBody]EmpleadoDto EmpleadoDto)
        {
            if (EmpleadoDto == null)
            {
                return NotFound();
            }
            var empleado = _mapper.Map<Empleado>(EmpleadoDto);
            _unitOfWork.Empleados.Update(empleado);
            await _unitOfWork.SaveAsync();
            return EmpleadoDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmpleadoDto>> Delete(int id)
        {
            var empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            _unitOfWork.Empleados.Remove(empleado);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}