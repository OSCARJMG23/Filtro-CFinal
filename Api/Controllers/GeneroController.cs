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
    public class GeneroController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public GeneroController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<GeneroDto>>> Get([FromQuery]Params generoParams)
        {
            var movimiento = await _unitOfWork.Generos.GetAllAsync(generoParams.PageIndex, generoParams.PageSize, generoParams.Search);
            var listaGenerosDto = _mapper.Map<List<GeneroDto>>(movimiento.registros);
            return new Pager<GeneroDto>(listaGenerosDto, movimiento.totalRegistros, generoParams.PageIndex, generoParams.PageSize, generoParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneroDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.Generos.GetByIdAsync(id);
            return _mapper.Map<GeneroDto>(movimiento);
        }
        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Genero>> Post(GeneroDto GeneroDto)
        {
            var movimiento = _mapper.Map<Genero>(GeneroDto);
            _unitOfWork.Generos.Add(movimiento);
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
        public async Task<ActionResult<GeneroDto>> Put(int id, [FromBody]GeneroDto GeneroDto)
        {
            if (GeneroDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<Genero>(GeneroDto);
            _unitOfWork.Generos.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return GeneroDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneroDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.Generos.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.Generos.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}