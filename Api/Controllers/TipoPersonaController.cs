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
    public class TipoPersonaController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public TipoPersonaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<TipoPersonaDto>>> Get([FromQuery]Params tipoPersonaParams)
        {
            var movimiento = await _unitOfWork.TipoPersonas.GetAllAsync(tipoPersonaParams.PageIndex, tipoPersonaParams.PageSize, tipoPersonaParams.Search);
            var listaTipoPersonaDto = _mapper.Map<List<TipoPersonaDto>>(movimiento.registros);
            return new Pager<TipoPersonaDto>(listaTipoPersonaDto, movimiento.totalRegistros, tipoPersonaParams.PageIndex, tipoPersonaParams.PageSize, tipoPersonaParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoPersonaDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.TipoPersonas.GetByIdAsync(id);
            return _mapper.Map<TipoPersonaDto>(movimiento);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoPersona>> Post(TipoPersonaDto TipoPersonaDto)
        {
            var movimiento = _mapper.Map<TipoPersona>(TipoPersonaDto);
            _unitOfWork.TipoPersonas.Add(movimiento);
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
        public async Task<ActionResult<TipoPersonaDto>> Put(int id, [FromBody]TipoPersonaDto TipoPersonaDto)
        {
            if (TipoPersonaDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<TipoPersona>(TipoPersonaDto);
            _unitOfWork.TipoPersonas.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return TipoPersonaDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoPersonaDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.TipoPersonas.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.TipoPersonas.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}