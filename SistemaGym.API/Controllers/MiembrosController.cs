using Microsoft.AspNetCore.Mvc;
using SistemaGym.API.Models;
using SistemaGym.API.Models.DTOs;
using SistemaGym.API.Services;

namespace SistemaGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiembrosController : ControllerBase
    {
        private readonly IMiembroService _service;

        public MiembrosController(IMiembroService service)
        {
            _service = service;
        }

        // GET: api/miembros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Miembro>>> GetMiembros()
        {
            return Ok(await _service.ObtenerTodos());
        }

        // GET: api/miembros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Miembro>> GetMiembro(int id)
        {
            var miembro = await _service.ObtenerPorId(id);

            if (miembro == null)
                return NotFound("Miembro no encontrado");

            return Ok(miembro);
        }

        // POST: api/miembros
        [HttpPost]
        public async Task<ActionResult<Miembro>> PostMiembro(Miembro miembro)
        {
            var nuevo = await _service.Crear(miembro);
            return CreatedAtAction(nameof(GetMiembro), new { id = nuevo.Id }, nuevo);
        }

        // GET: api/miembros/validar/1
        [HttpGet("validar/{id}")]
        public async Task<IActionResult> ValidarAcceso(int id)
        {
            var resultado = await _service.ValidarAcceso(id);

            if (!resultado.permitido)
                return BadRequest(resultado.mensaje);

            return Ok(resultado.mensaje);
        }

        // GET: api/miembros/1/accesos
        [HttpGet("{id}/accesos")]
        public async Task<IActionResult> ObtenerAccesos(int id)
        {
            var accesos = await _service.ObtenerAccesos(id);

            if (accesos == null)
                return NotFound("Miembro no encontrado");

            return Ok(accesos);
        }

        // POST: api/miembros/1/pagar
        [HttpPost("{id}/pagar")]
        public async Task<IActionResult> RegistrarPago(int id, [FromBody] PagoRequest request)
        {
            var resultado = await _service.RegistrarPago(id, request);

            if (!resultado.exito)
                return NotFound(resultado.mensaje);

            return Ok(new
            {
                mensaje = resultado.mensaje,
                nuevaFechaVencimiento = resultado.nuevaFecha
            });
        }
    }
}