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
    public class FormaPagoController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public FormaPagoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<FormaPagoDto>>> Get([FromQuery]Params formaPagoParams)
        {
            var movimiento = await _unitOfWork.FormaPagos.GetAllAsync(formaPagoParams.PageIndex, formaPagoParams.PageSize, formaPagoParams.Search);
            var listaFormaPagoDto = _mapper.Map<List<FormaPagoDto>>(movimiento.registros);
            return new Pager<FormaPagoDto>(listaFormaPagoDto, movimiento.totalRegistros, formaPagoParams.PageIndex, formaPagoParams.PageSize, formaPagoParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FormaPagoDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.FormaPagos.GetByIdAsync(id);
            return _mapper.Map<FormaPagoDto>(movimiento);
        }
        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FormaPago>> Post(FormaPagoDto FormaPagoDto)
        {
            var formaPago = _mapper.Map<FormaPago>(FormaPagoDto);
            _unitOfWork.FormaPagos.Add(formaPago);
            await _unitOfWork.SaveAsync();
        
            if (formaPago == null)
            {
                return BadRequest();
            }
            formaPago.Id = formaPago.Id;
            return CreatedAtAction(nameof(Post), new { id = formaPago.Id }, formaPago);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FormaPagoDto>> Put(int id, [FromBody]FormaPagoDto FormaPagoDto)
        {
            if (FormaPagoDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<FormaPago>(FormaPagoDto);
            _unitOfWork.FormaPagos.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return FormaPagoDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FormaPagoDto>> Delete(int id)
        {
            var movimiento = await _unitOfWork.FormaPagos.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.FormaPagos.Remove(movimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}