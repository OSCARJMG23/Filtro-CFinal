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
    
    public class CargoController :BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        
        public CargoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<CargoDto>>> Get([FromQuery]Params cargoParams)
        {
            var movimiento = await _unitOfWork.Cargos.GetAllAsync(cargoParams.PageIndex, cargoParams.PageSize, cargoParams.Search);
            var listaCargosDto = _mapper.Map<List<CargoDto>>(movimiento.registros);
            return new Pager<CargoDto>(listaCargosDto, movimiento.totalRegistros, cargoParams.PageIndex, cargoParams.PageSize, cargoParams.Search);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CargoDto>> Get(int id)
        {
            var movimiento = await _unitOfWork.Cargos.GetByIdAsync(id);
            return _mapper.Map<CargoDto>(movimiento);
        }
        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cargo>> Post(CargoDto CargoDto)
        {
            var cargo = _mapper.Map<Cargo>(CargoDto);
            _unitOfWork.Cargos.Add(cargo);
            await _unitOfWork.SaveAsync();
        
            if (cargo == null)
            {
                return BadRequest();
            }
            cargo.Id = cargo.Id;
            return CreatedAtAction(nameof(Post), new { id = cargo.Id }, cargo);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CargoDto>> Put(int id, [FromBody]CargoDto CargoDto)
        {
            if (CargoDto == null)
            {
                return NotFound();
            }
            var movimiento = _mapper.Map<Cargo>(CargoDto);
            _unitOfWork.Cargos.Update(movimiento);
            await _unitOfWork.SaveAsync();
            return CargoDto;
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CargoDto>> Delete(int id)
        {
            var cargo = await _unitOfWork.Cargos.GetByIdAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            _unitOfWork.Cargos.Remove(cargo);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}