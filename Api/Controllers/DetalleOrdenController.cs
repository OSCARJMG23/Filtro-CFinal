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
    public class DetalleOrdenController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public DetalleOrdenController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<DetalleOrdenDto>>> Get([FromQuery]Params detalleOrdenParams)
        {
            var DetalleOrden = await _unitOfWork.DetalleOrdenes.GetAllAsync(detalleOrdenParams.PageIndex, detalleOrdenParams.PageSize, detalleOrdenParams.Search);
            var listaDetalleOrdenDto = _mapper.Map<List<DetalleOrdenDto>>(DetalleOrden.registros);
            return new Pager<DetalleOrdenDto>(listaDetalleOrdenDto, DetalleOrden.totalRegistros, detalleOrdenParams.PageIndex, detalleOrdenParams.PageSize, detalleOrdenParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetalleOrdenDto>> Get(int id)
        {
            var detalleOrden = await _unitOfWork.DetalleOrdenes.GetByIdAsync(id);
            return _mapper.Map<DetalleOrdenDto>(detalleOrden);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetalleOrden>> Post(DetalleOrdenDto DetalleOrdenDto)
        {
            var detalleOrden = _mapper.Map<DetalleOrden>(DetalleOrdenDto);
            _unitOfWork.DetalleOrdenes.Add(detalleOrden);
            await _unitOfWork.SaveAsync();
        
            if (detalleOrden == null)
            {
                return BadRequest();
            }
            detalleOrden.Id = detalleOrden.Id;
            return CreatedAtAction(nameof(Post), new { id = detalleOrden.Id }, detalleOrden);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetalleOrdenDto>> Put(int id, [FromBody]DetalleOrdenDto DetalleOrdenDto)
        {
            if (DetalleOrdenDto == null)
            {
                return NotFound();
            }
            var detalleOrden = _mapper.Map<DetalleOrden>(DetalleOrdenDto);
            _unitOfWork.DetalleOrdenes.Update(detalleOrden);
            await _unitOfWork.SaveAsync();
            return DetalleOrdenDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetalleOrdenDto>> Delete(int id)
        {
            var detalleOrden = await _unitOfWork.DetalleOrdenes.GetByIdAsync(id);
            if (detalleOrden == null)
            {
                return NotFound();
            }
            _unitOfWork.DetalleOrdenes.Remove(detalleOrden);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}