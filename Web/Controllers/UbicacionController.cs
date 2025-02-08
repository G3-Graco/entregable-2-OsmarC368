using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UbicacionController : ControllerBase
    {
        private IUbicacionService _servicio;

        public UbicacionController(IUbicacionService ubicacionService)
        {
            _servicio = ubicacionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ubicacion>>> Get()
        {
            var equipos = await _servicio.GetAll();

            return Ok(equipos);
        }

        [HttpPost]
        public async Task<ActionResult<Ubicacion>> Post([FromBody] Ubicacion ubicacion)
        {
            try
            {
                var createdUbicacion = await _servicio.Create(ubicacion);
                return Ok(createdUbicacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Ubicacion>> Delete(int id)
        {
            try
            {
                await _servicio.Delete(id);
                return Ok("Ubicacion eliminada");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Ubicacion>> Update(int id, [FromBody] Ubicacion ubicacion)
        {
            try
            {
                await _servicio.Update(id, ubicacion);
                return Ok("Ubicacion Actualizada!!!!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}