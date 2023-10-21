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
    public class TallaController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public TallaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<TallaDto>>> Get([FromQuery]Params tallaParams)
        {
            var movimiento = await _unitOfWork.Tallas.GetAllAsync(tallaParams.PageIndex, tallaParams.PageSize, tallaParams.Search);
            var listaTallaDto = _mapper.Map<List<TallaDto>>(movimiento.registros);
            return new Pager<TallaDto>(listaTallaDto, movimiento.totalRegistros, tallaParams.PageIndex, tallaParams.PageSize, tallaParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TallaDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.Tallas.GetByIdAsync(id);
            return _mapper.Map<TallaDto>(movimiento);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Talla>> Post(TallaDto TallaDto)
        {
            var movimiento = _mapper.Map<Talla>(TallaDto);
            _unitOfWork.Tallas.Add(movimiento);
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
        public async Task<ActionResult<TallaDto>> Put(int id, [FromBody]TallaDto TallaDto)
        {
            if (TallaDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<Talla>(TallaDto);
            _unitOfWork.Tallas.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return TallaDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TallaDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.Tallas.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.Tallas.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}