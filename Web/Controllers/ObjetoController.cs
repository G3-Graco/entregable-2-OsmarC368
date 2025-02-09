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
    public class ObjetoController : ControllerBase
    {
        private IObjetoService _servicio;

        public ObjetoController(IObjetoService objetoService)
        {
            _servicio = objetoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Objeto>>> Get()
        {
            var objetos = await _servicio.GetAll();

            return Ok(objetos);
        }

        [HttpPost]
        public async Task<ActionResult<Objeto>> Post([FromBody] Objeto objeto)
        {
            try
            {
                var createdObjeto = await _servicio.Create(objeto);
                return Ok(createdObjeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Objeto>> Delete(int id)
        {
            try
            {
                await _servicio.Delete(id);
                return Ok("Objeto eliminado");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Objeto>> Update(int id, [FromBody] Objeto objeto)
        {
            try
            {
                await _servicio.Update(id, objeto);
                return Ok("Objeto Actualizado!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}