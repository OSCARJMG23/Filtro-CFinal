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
    public class InsumoController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public InsumoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<InsumoDto>>> Get([FromQuery]Params movimientoParams)
        {
            var movimiento = await _unitOfWork.Insumos.GetAllAsync(movimientoParams.PageIndex, movimientoParams.PageSize, movimientoParams.Search);
            var listaMovimientosDto = _mapper.Map<List<InsumoDto>>(movimiento.registros);
            return new Pager<InsumoDto>(listaMovimientosDto, movimiento.totalRegistros, movimientoParams.PageIndex, movimientoParams.PageSize, movimientoParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InsumoDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.Insumos.GetByIdAsync(id);
            return _mapper.Map<InsumoDto>(movimiento);
        }
        [HttpGet("Xprenda/{codigoPrenda}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<InsumoDto>>> GetInsumoPrenda(int codigoPrenda)
        {
            var movimiento = await _unitOfWork.Insumos.InsumosXPrenda(codigoPrenda);
            return _mapper.Map<List<InsumoDto>>(movimiento);
        }
        [HttpGet("Xproveedor/{nitProveedor}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<InsumoDto>>> GetInsumoProveedor(int nitProveedor)
        {
            var movimiento = await _unitOfWork.Insumos.InsumosXProveedor(nitProveedor);
            return _mapper.Map<List<InsumoDto>>(movimiento);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Insumo>> Post(InsumoDto InsumoDto)
        {
            var movimiento = _mapper.Map<Insumo>(InsumoDto);
            _unitOfWork.Insumos.Add(movimiento);
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
        public async Task<ActionResult<InsumoDto>> Put(int id, [FromBody]InsumoDto InsumoDto)
        {
            if (InsumoDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<Insumo>(InsumoDto);
            _unitOfWork.Insumos.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return InsumoDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InsumoDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.Insumos.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.Insumos.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}