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
    public class OrdenController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public OrdenController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<OrdenDto>>> Get([FromQuery]Params ordenParams)
        {
            var movimiento = await _unitOfWork.Ordenes.GetAllAsync(ordenParams.PageIndex, ordenParams.PageSize, ordenParams.Search);
            var listaOrdenDto = _mapper.Map<List<OrdenDto>>(movimiento.registros);
            return new Pager<OrdenDto>(listaOrdenDto, movimiento.totalRegistros, ordenParams.PageIndex, ordenParams.PageSize, ordenParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrdenDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.Ordenes.GetByIdAsync(id);
            return _mapper.Map<OrdenDto>(movimiento);
        }
        [HttpGet("proceso")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrdenProcesoDto>>> GetOrdenProceso()
        {
            var movimiento = await _unitOfWork.Ordenes.OrdenesEnProceso();
            return _mapper.Map<List<OrdenProcesoDto>>(movimiento);
        }
        [HttpGet("cliente/{idClienteconsulta}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrdenDto>>> GetOrdenCliente(int idClienteConsulta)
        {
            var movimiento = await _unitOfWork.Ordenes.OrdenXCliente(idClienteConsulta);
            return _mapper.Map<List<OrdenDto>>(movimiento);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Orden>> Post(OrdenDto OrdenDto)
        {
            var movimiento = _mapper.Map<Orden>(OrdenDto);
            _unitOfWork.Ordenes.Add(movimiento);
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
        public async Task<ActionResult<OrdenDto>> Put(int id, [FromBody]OrdenDto OrdenDto)
        {
            if (OrdenDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<Orden>(OrdenDto);
            _unitOfWork.Ordenes.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return OrdenDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrdenDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.Ordenes.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.Ordenes.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}