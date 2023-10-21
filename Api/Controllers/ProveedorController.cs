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
    public class ProveedorController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public ProveedorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<ProveedorDto>>> Get([FromQuery]Params proveedorParams)
        {
            var movimiento = await _unitOfWork.Proveedores.GetAllAsync(proveedorParams.PageIndex, proveedorParams.PageSize, proveedorParams.Search);
            var listaProveedorDto = _mapper.Map<List<ProveedorDto>>(movimiento.registros);
            return new Pager<ProveedorDto>(listaProveedorDto, movimiento.totalRegistros, proveedorParams.PageIndex, proveedorParams.PageSize, proveedorParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProveedorDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.Proveedores.GetByIdAsync(id);
            return _mapper.Map<ProveedorDto>(movimiento);
        }
        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Proveedor>> Post(ProveedorDto ProveedorDto)
        {
            var movimiento = _mapper.Map<Proveedor>(ProveedorDto);
            _unitOfWork.Proveedores.Add(movimiento);
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
        public async Task<ActionResult<ProveedorDto>> Put(int id, [FromBody]ProveedorDto ProveedorDto)
        {
            if (ProveedorDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<Proveedor>(ProveedorDto);
            _unitOfWork.Proveedores.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return ProveedorDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProveedorDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.Proveedores.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.Proveedores.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}