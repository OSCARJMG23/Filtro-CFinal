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
    public class EstadoController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public EstadoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<EstadoDto>>> Get([FromQuery]Params estadoParams)
        {
            var movimiento = await _unitOfWork.Estados.GetAllAsync(estadoParams.PageIndex, estadoParams.PageSize, estadoParams.Search);
            var listaEstadosDto = _mapper.Map<List<EstadoDto>>(movimiento.registros);
            return new Pager<EstadoDto>(listaEstadosDto, movimiento.totalRegistros, estadoParams.PageIndex, estadoParams.PageSize, estadoParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EstadoDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.Estados.GetByIdAsync(id);
            return _mapper.Map<EstadoDto>(movimiento);
        }
        

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Estado>> Post(EstadoDto EstadoDto)
        {
            var estado = _mapper.Map<Estado>(EstadoDto);
            _unitOfWork.Estados.Add(estado);
            await _unitOfWork.SaveAsync();
        
            if (estado == null)
            {
                return BadRequest();
            }
            estado.Id = estado.Id;
            return CreatedAtAction(nameof(Post), new { id = estado.Id }, estado);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EstadoDto>> Put(int id, [FromBody]EstadoDto EstadoDto)
        {
            if (EstadoDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<Estado>(EstadoDto);
            _unitOfWork.Estados.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return EstadoDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EstadoDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.Estados.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.Estados.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}