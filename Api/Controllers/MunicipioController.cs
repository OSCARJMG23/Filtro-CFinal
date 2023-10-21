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
    public class MunicipioController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public MunicipioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<MunicipioDto>>> Get([FromQuery]Params municipioParams)
        {
            var movimiento = await _unitOfWork.Municipios.GetAllAsync(municipioParams.PageIndex, municipioParams.PageSize, municipioParams.Search);
            var listaMunicipiosDto = _mapper.Map<List<MunicipioDto>>(movimiento.registros);
            return new Pager<MunicipioDto>(listaMunicipiosDto, movimiento.totalRegistros, municipioParams.PageIndex, municipioParams.PageSize, municipioParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MunicipioDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.Municipios.GetByIdAsync(id);
            return _mapper.Map<MunicipioDto>(movimiento);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Municipio>> Post(MunicipioDto MunicipioDto)
        {
            var movimiento = _mapper.Map<Municipio>(MunicipioDto);
            _unitOfWork.Municipios.Add(movimiento);
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
        public async Task<ActionResult<MunicipioDto>> Put(int id, [FromBody]MunicipioDto MunicipioDto)
        {
            if (MunicipioDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<Municipio>(MunicipioDto);
            _unitOfWork.Municipios.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return MunicipioDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MunicipioDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.Municipios.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.Municipios.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        
    }
}