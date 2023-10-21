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
    public class TipoProteccionController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public TipoProteccionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<TipoProteccionDto>>> Get([FromQuery]Params tipoProteccionParams)
        {
            var movimiento = await _unitOfWork.TipoProtecciones.GetAllAsync(tipoProteccionParams.PageIndex, tipoProteccionParams.PageSize, tipoProteccionParams.Search);
            var listaTipoProteccionDto = _mapper.Map<List<TipoProteccionDto>>(movimiento.registros);
            return new Pager<TipoProteccionDto>(listaTipoProteccionDto, movimiento.totalRegistros, tipoProteccionParams.PageIndex, tipoProteccionParams.PageSize, tipoProteccionParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoProteccionDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.TipoProtecciones.GetByIdAsync(id);
            return _mapper.Map<TipoProteccionDto>(movimiento);
        }
        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoProteccion>> Post(TipoProteccionDto TipoProteccionDto)
        {
            var movimiento = _mapper.Map<TipoProteccion>(TipoProteccionDto);
            _unitOfWork.TipoProtecciones.Add(movimiento);
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
        public async Task<ActionResult<TipoProteccionDto>> Put(int id, [FromBody]TipoProteccionDto TipoProteccionDto)
        {
            if (TipoProteccionDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<TipoProteccion>(TipoProteccionDto);
            _unitOfWork.TipoProtecciones.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return TipoProteccionDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoProteccionDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.TipoProtecciones.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.TipoProtecciones.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}