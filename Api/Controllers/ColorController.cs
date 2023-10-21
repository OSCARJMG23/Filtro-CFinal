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

    public class ColorController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public ColorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<ColorDto>>> Get([FromQuery]Params movimientoParams)
        {
            var movimiento = await _unitOfWork.Colores.GetAllAsync(movimientoParams.PageIndex, movimientoParams.PageSize, movimientoParams.Search);
            var listaMovimientosDto = _mapper.Map<List<ColorDto>>(movimiento.registros);
            return new Pager<ColorDto>(listaMovimientosDto, movimiento.totalRegistros, movimientoParams.PageIndex, movimientoParams.PageSize, movimientoParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ColorDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.Colores.GetByIdAsync(id);
            return _mapper.Map<ColorDto>(movimiento);
        }
        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Color>> Post(ColorDto ColorDto)
        {
            var color = _mapper.Map<Color>(ColorDto);
            _unitOfWork.Colores.Add(color);
            await _unitOfWork.SaveAsync();
        
            if (color == null)
            {
                return BadRequest();
            }
            color.Id = color.Id;
            return CreatedAtAction(nameof(Post), new { id = color.Id }, color);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ColorDto>> Put(int id, [FromBody]ColorDto ColorDto)
        {
            if (ColorDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<Color>(ColorDto);
            _unitOfWork.Colores.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return ColorDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ColorDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.Colores.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.Colores.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}