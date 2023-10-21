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
    public class PrendaController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public PrendaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<PrendaDto>>> Get([FromQuery]Params prendaParams)
        {
            var movimiento = await _unitOfWork.Prendas.GetAllAsync(prendaParams.PageIndex, prendaParams.PageSize, prendaParams.Search);
            var listaPrendasDto = _mapper.Map<List<PrendaDto>>(movimiento.registros);
            return new Pager<PrendaDto>(listaPrendasDto, movimiento.totalRegistros, prendaParams.PageIndex, prendaParams.PageSize, prendaParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PrendaDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.Prendas.GetByIdAsync(id);
            return _mapper.Map<PrendaDto>(movimiento);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Prenda>> Post(PrendaDto PrendaDto)
        {
            var movimiento = _mapper.Map<Prenda>(PrendaDto);
            _unitOfWork.Prendas.Add(movimiento);
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
        public async Task<ActionResult<PrendaDto>> Put(int id, [FromBody]PrendaDto PrendaDto)
        {
            if (PrendaDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<Prenda>(PrendaDto);
            _unitOfWork.Prendas.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return PrendaDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PrendaDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.Prendas.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.Prendas.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}