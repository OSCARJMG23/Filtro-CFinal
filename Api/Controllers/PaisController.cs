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
    public class PaisController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public PaisController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<PaisDto>>> Get([FromQuery]Params PaisParams)
        {
            var movimiento = await _unitOfWork.Paises.GetAllAsync(PaisParams.PageIndex, PaisParams.PageSize, PaisParams.Search);
            var listaPaisDto = _mapper.Map<List<PaisDto>>(movimiento.registros);
            return new Pager<PaisDto>(listaPaisDto, movimiento.totalRegistros, PaisParams.PageIndex, PaisParams.PageSize, PaisParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaisDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.Paises.GetByIdAsync(id);
            return _mapper.Map<PaisDto>(movimiento);
        }
        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pais>> Post(PaisDto PaisDto)
        {
            var movimiento = _mapper.Map<Pais>(PaisDto);
            _unitOfWork.Paises.Add(movimiento);
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
        public async Task<ActionResult<PaisDto>> Put(int id, [FromBody]PaisDto PaisDto)
        {
            if (PaisDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<Pais>(PaisDto);
            _unitOfWork.Paises.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return PaisDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaisDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.Paises.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.Paises.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}