using AutoMapper;
using ContpaqiAPI.Entities;
using ContpaqiAPI.Models;
using ContpaqiAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContpaqiAPI.Controllers
{
    [ApiController]
    [Route("api/empleado")]
    public class EmpleadoController : ControllerBase
    {
        private IEmpleadoInfoRepository _empleadoinforepository;
        private IMapper _mapper;

        public EmpleadoController(IEmpleadoInfoRepository empleadoinforepository, IMapper mapper)
        {
            _empleadoinforepository = empleadoinforepository ?? throw new ArgumentNullException(nameof(empleadoinforepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetEmpleados()
        {
            try
            {
                var empleados = _empleadoinforepository.GetEmpleados();
                return Ok(_mapper.Map<IEnumerable<EmpleadoSKDto>>(empleados));
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"ocurrio un error al guardar en base el sig cast:{finalcast} en el movieid {movieId}", finalcast, movieId);
                return BadRequest();
            }
        }

        [HttpGet("{Id}", Name = "GetEmpleado")]
        public IActionResult GetEmpleado(int Id)
        {
            try
            {
                var empleado = _empleadoinforepository.GetEmpleado(Id);

                if (empleado == null)
                    return NotFound();


                return Ok(_mapper.Map<EmpleadoSKDto>(empleado));
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"ocurrio un error al guardar en base el sig cast:{finalcast} en el movieid {movieId}", finalcast, movieId);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CreateEmpleado([FromBody] EmpleadoINDto empleado)
        {
            try
            {
                var finalcast = _mapper.Map<Empleado>(empleado);
                Empleado empleadoReturn = _empleadoinforepository.AddEmpleado(finalcast);

                var createdCastToReturn = _mapper.Map<EmpleadoSKDto>(empleadoReturn);

                return CreatedAtRoute(
                nameof(GetEmpleado),
                new { Id = createdCastToReturn.EmpleadoId },
                    createdCastToReturn
                );
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"ocurrio un error al guardar en base el sig cast:{finalcast} en el movieid {movieId}", finalcast, movieId);
                return BadRequest();
            }

        }

        [HttpPut]
        public IActionResult UpdateEmpleado([FromBody] EmpleadoUPDto empleado)
        {
            try
            {
                if (empleado.EstadoCivil == null
                    && empleado.Direccion == null
                    && empleado.Email == null
                    && empleado.FechaBaja == null
                    && empleado.Puesto == null
                    && empleado.Telefono == null)
                    return BadRequest();

                var finalcast = _mapper.Map<Empleado>(empleado);

                Empleado empleadoReturn = _empleadoinforepository.UpdateEmpleado(finalcast);

                if (empleadoReturn.EmpleadoId == 0)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"ocurrio un error al guardar en base el sig cast:{finalcast} en el movieid {movieId}", finalcast, movieId);
                return BadRequest();
            }

        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteEmpleado(int Id)
        {
            try
            {
                var empleado = _empleadoinforepository.DeleteEmpleado(Id);

                if (empleado.EmpleadoId == 0)
                    return NotFound();


                return NoContent();
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"ocurrio un error al guardar en base el sig cast:{finalcast} en el movieid {movieId}", finalcast, movieId);
                return BadRequest();
            }
        }

    }
}
