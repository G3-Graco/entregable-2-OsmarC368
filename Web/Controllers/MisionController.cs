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
    public class MisionController : ControllerBase
    {
        private IMisionService _servicio;

        public MisionController(IMisionService misionService)
        {
            _servicio = misionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mision>>> Get()
        {
            var misiones = await _servicio.GetAll();

            return Ok(misiones);
        }

        [HttpPost]
        public async Task<ActionResult<Mision>> Post([FromBody] Mision mision)
        {
            try
            {
                var createdMision = await _servicio.Create(mision);
                return Ok(createdMision);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Mision>> Delete(int id)
        {
            try
            {
                await _servicio.Delete(id);
                return Ok("Mision eliminada");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Mision>> Update(int id, Mision mision)
        {
            try
            {
                await _servicio.Update(id, mision);
                return Ok("Mision Actualizada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}