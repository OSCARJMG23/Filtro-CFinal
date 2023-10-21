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
    public class DetalleVentaController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public DetalleVentaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<DetalleVentaDto>>> Get([FromQuery]Params detalleVentaParams)
        {
            var DetalleVenta = await _unitOfWork.DetalleVentas.GetAllAsync(detalleVentaParams.PageIndex, detalleVentaParams.PageSize, detalleVentaParams.Search);
            var listadetalleVentaDto = _mapper.Map<List<DetalleVentaDto>>(DetalleVenta.registros);
            return new Pager<DetalleVentaDto>(listadetalleVentaDto, DetalleVenta.totalRegistros, detalleVentaParams.PageIndex, detalleVentaParams.PageSize, detalleVentaParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetalleVentaDto>> Get(int id)
        {
            var detalleVenta = await _unitOfWork.DetalleVentas.GetByIdAsync(id);
            return _mapper.Map<DetalleVentaDto>(detalleVenta);
        }
         
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetalleVenta>> Post(DetalleVentaDto DetalleVentaDto)
        {
            var detalleVenta = _mapper.Map<DetalleVenta>(DetalleVentaDto);
            _unitOfWork.DetalleVentas.Add(detalleVenta);
            await _unitOfWork.SaveAsync();
        
            if (detalleVenta == null)
            {
                return BadRequest();
            }
            detalleVenta.Id = detalleVenta.Id;
            return CreatedAtAction(nameof(Post), new { id = detalleVenta.Id }, detalleVenta);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetalleVentaDto>> Put(int id, [FromBody]DetalleVentaDto DetalleVentaDto)
        {
            if (DetalleVentaDto == null)
            {
                return NotFound();
            }
            var detalleVenta = _mapper.Map<DetalleVenta>(DetalleVentaDto);
            _unitOfWork.DetalleVentas.Update(detalleVenta);
            await _unitOfWork.SaveAsync();
            return DetalleVentaDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetalleVentaDto>> Delete(int id)
        {
            var detalleVenta = await _unitOfWork.DetalleVentas.GetByIdAsync(id);
            if (detalleVenta == null)
            {
                return NotFound();
            }
            _unitOfWork.DetalleVentas.Remove(detalleVenta);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}