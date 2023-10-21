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
    public class VentaController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public VentaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<VentaDto>>> Get([FromQuery]Params ventaParams)
        {
            var movimiento = await _unitOfWork.Ventas.GetAllAsync(ventaParams.PageIndex, ventaParams.PageSize, ventaParams.Search);
            var listaVentasDto = _mapper.Map<List<VentaDto>>(movimiento.registros);
            return new Pager<VentaDto>(listaVentasDto, movimiento.totalRegistros, ventaParams.PageIndex, ventaParams.PageSize, ventaParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VentaDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.Ventas.GetByIdAsync(id);
            return _mapper.Map<VentaDto>(movimiento);
        }
        [HttpGet("empleado/{idEmpleado}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VentaDto>>> GetVentaEmpleado(double idVentaConsulta)
        {
            var movimiento = await _unitOfWork.Ventas.VentaXEmpleado(idVentaConsulta);
            return _mapper.Map<List<VentaDto>>(movimiento);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Venta>> Post(VentaDto VentaDto)
        {
            var movimiento = _mapper.Map<Venta>(VentaDto);
            _unitOfWork.Ventas.Add(movimiento);
            await _unitOfWork.SaveAsync();
        
            if (movimiento == null)
            {
                return BadRequest();
            }
            movimiento.Id = movimiento.Id;
            return CreatedAtAction(nameof(Post), new { id = movimiento.Id }, movimiento);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VentaDto>> Put(int id, [FromBody]VentaDto VentaDto)
        {
            if (VentaDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<Venta>(VentaDto);
            _unitOfWork.Ventas.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return VentaDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VentaDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.Ventas.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.Ventas.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}