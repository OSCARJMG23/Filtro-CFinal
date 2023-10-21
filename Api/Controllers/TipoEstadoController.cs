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
    public class TipoEstadoController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public TipoEstadoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<TipoEstadoDto>>> Get([FromQuery]Params tipoEstadoParams)
        {
            var movimiento = await _unitOfWork.TipoEstados.GetAllAsync(tipoEstadoParams.PageIndex, tipoEstadoParams.PageSize, tipoEstadoParams.Search);
            var listaTipoEstadoDto = _mapper.Map<List<TipoEstadoDto>>(movimiento.registros);
            return new Pager<TipoEstadoDto>(listaTipoEstadoDto, movimiento.totalRegistros, tipoEstadoParams.PageIndex, tipoEstadoParams.PageSize, tipoEstadoParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoEstadoDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.TipoEstados.GetByIdAsync(id);
            return _mapper.Map<TipoEstadoDto>(movimiento);
        }
        

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoEstado>> Post(TipoEstadoDto TipoEstadoDto)
        {
            var movimiento = _mapper.Map<TipoEstado>(TipoEstadoDto);
            _unitOfWork.TipoEstados.Add(movimiento);
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
        public async Task<ActionResult<TipoEstadoDto>> Put(int id, [FromBody]TipoEstadoDto TipoEstadoDto)
        {
            if (TipoEstadoDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<TipoEstado>(TipoEstadoDto);
            _unitOfWork.TipoEstados.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return TipoEstadoDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoEstadoDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.TipoEstados.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.TipoEstados.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}