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
    public class EquipoController : ControllerBase
    {
        private IEquipoService _servicio;

        public EquipoController(IEquipoService equipoService)
        {
            _servicio = equipoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipo>>> Get()
        {
            var equipos = await _servicio.GetAll();

            return Ok(equipos);
        }

        [HttpPost]
        public async Task<ActionResult<Equipo>> Post([FromBody] Equipo equipo)
        {
            try
            {
                var createdEquipo = await _servicio.Create(equipo);
                return Ok(createdEquipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Equipo>> Delete(int id)
        {
            try
            {
                await _servicio.Delete(id);
                return Ok("Equipo eliminado");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Equipo>> Update(int id, [FromBody] Equipo equipo)
        {
            try
            {
                await _servicio.Update(id, equipo);
                return Ok("Equipo Actualizado!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Equipar/{idEquipo}/{idObjeto}/{type}")]
        public async Task<ActionResult<Equipo>> Equipar(int idEquipo, int idObjeto, string type)
        {
            try
            {
                await _servicio.EquiparObjeto(idObjeto, idEquipo, type);
                return Ok("Objeto Equipado!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Desequipar/{idEquipo}/{idObjeto}")]
        public async Task<ActionResult<Equipo>> Desequipar(int idEquipo, int idObjeto)
        {
            try
            {
                await _servicio.DesequiparObjeto(idObjeto, idEquipo);
                return Ok("Objeto Desequipado!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}