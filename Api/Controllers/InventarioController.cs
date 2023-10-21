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
    public class InventarioController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public InventarioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<InventarioDto>>> Get([FromQuery]Params inventarioParams)
        {
            var movimiento = await _unitOfWork.Inventarios.GetAllAsync(inventarioParams.PageIndex, inventarioParams.PageSize, inventarioParams.Search);
            var listaInventarioDto = _mapper.Map<List<InventarioDto>>(movimiento.registros);
            return new Pager<InventarioDto>(listaInventarioDto, movimiento.totalRegistros, inventarioParams.PageIndex, inventarioParams.PageSize, inventarioParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventarioDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.Inventarios.GetByIdAsync(id);
            return _mapper.Map<InventarioDto>(movimiento);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Inventario>> Post(InventarioDto InventarioDto)
        {
            var movimiento = _mapper.Map<Inventario>(InventarioDto);
            _unitOfWork.Inventarios.Add(movimiento);
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
        public async Task<ActionResult<InventarioDto>> Put(int id, [FromBody]InventarioDto InventarioDto)
        {
            if (InventarioDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<Inventario>(InventarioDto);
            _unitOfWork.Inventarios.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return InventarioDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventarioDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.Inventarios.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.Inventarios.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}