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
    public class EmpresaController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public EmpresaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<EmpresaDto>>> Get([FromQuery]Params empresaParams)
        {
            var movimiento = await _unitOfWork.Empresas.GetAllAsync(empresaParams.PageIndex, empresaParams.PageSize, empresaParams.Search);
            var listaEmpresasDto = _mapper.Map<List<EmpresaDto>>(movimiento.registros);
            return new Pager<EmpresaDto>(listaEmpresasDto, movimiento.totalRegistros, empresaParams.PageIndex, empresaParams.PageSize, empresaParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmpresaDto>> Get(int id)
        {
            var empresa = await _unitOfWork.Empresas.GetByIdAsync(id);
            return _mapper.Map<EmpresaDto>(empresa);
        }        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Empresa>> Post(EmpresaDto EmpresaDto)
        {
            var empresa = _mapper.Map<Empresa>(EmpresaDto);
            _unitOfWork.Empresas.Add(empresa);
            await _unitOfWork.SaveAsync();
        
            if (empresa == null)
            {
                return BadRequest();
            }
            empresa.Id = empresa.Id;
            return CreatedAtAction(nameof(Post), new { id = empresa.Id }, empresa);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmpresaDto>> Put(int id, [FromBody]EmpresaDto EmpresaDto)
        {
            if (EmpresaDto == null)
            {
                return NotFound();
            }
            var empresa = _mapper.Map<Empresa>(EmpresaDto);
            _unitOfWork.Empresas.Update(empresa);
            await _unitOfWork.SaveAsync();
            return EmpresaDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmpresaDto>> Delete(int id)
        {
            var empresa = await _unitOfWork.Empresas.GetByIdAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            _unitOfWork.Empresas.Remove(empresa);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}