using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGym.API.Data;
using SistemaGym.API.Models;
using SistemaGym.API.Models.DTOs;

namespace SistemaGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiembrosController : ControllerBase
    {
        private readonly GymDbContext _context;

        public MiembrosController(GymDbContext context)
        {
            _context = context;
        }

        // GET: api/miembros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Miembro>>> GetMiembros()
        {
            return await _context.Miembros.ToListAsync();
        }

        // GET: api/miembros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Miembro>> GetMiembro(int id)
        {
            var miembro = await _context.Miembros.FindAsync(id);

            if (miembro == null)
                return NotFound();

            return miembro;
        }

        // POST: api/miembros
        [HttpPost]
        public async Task<ActionResult<Miembro>> PostMiembro(Miembro miembro)
        {
            _context.Miembros.Add(miembro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMiembro), new { id = miembro.Id }, miembro);
        }

        // GET: api/miembros/validar/1
        [HttpGet("validar/{id}")]
        public async Task<IActionResult> ValidarAcceso(int id)
        {
            var miembro = await _context.Miembros.FindAsync(id);

            if (miembro == null)
                return NotFound("Miembro no encontrado");

            bool permitido = true;
            string mensaje = "Acceso permitido";

            if (!miembro.Activo)
            {
                permitido = false;
                mensaje = "Membresía inactiva";
            }
            else if (miembro.FechaVencimiento < DateTime.Now)
            {
                permitido = false;
                mensaje = "Membresía vencida";
            }

            var acceso = new Acceso
            {
                MiembroId = id,
                AccesoPermitido = permitido,
                Mensaje = mensaje,
                FechaHora = DateTime.Now
            };

            _context.Accesos.Add(acceso);
            await _context.SaveChangesAsync();

            if (!permitido)
                return BadRequest(mensaje);

            return Ok(mensaje);
        }

        // GET: api/miembros/1/accesos
        [HttpGet("{id}/accesos")]
        public async Task<ActionResult<IEnumerable<Acceso>>> ObtenerAccesos(int id)
        {
            var existe = await _context.Miembros.AnyAsync(m => m.Id == id);

            if (!existe)
                return NotFound("Miembro no encontrado");

            var accesos = await _context.Accesos
                .Where(a => a.MiembroId == id)
                .OrderByDescending(a => a.FechaHora)
                .ToListAsync();

            return Ok(accesos);
        }

        // POST: api/miembros/1/pagar
        [HttpPost("{id}/pagar")]
        public async Task<IActionResult> RegistrarPago(int id, [FromBody] PagoRequest request)
        {
            var miembro = await _context.Miembros.FindAsync(id);

            if (miembro == null)
                return NotFound("Miembro no encontrado");

            var pago = new Pago
            {
                MiembroId = id,
                Monto = request.Monto,
                MesesPagados = request.MesesPagados,
                Observaciones = request.Observaciones,
                FechaPago = DateTime.Now
            };

            _context.Pagos.Add(pago);

            if (miembro.FechaVencimiento < DateTime.Now)
                miembro.FechaVencimiento = DateTime.Now.AddMonths(request.MesesPagados);
            else
                miembro.FechaVencimiento = miembro.FechaVencimiento.AddMonths(request.MesesPagados);

            miembro.Activo = true;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Pago registrado y membresía renovada",
                nuevaFechaVencimiento = miembro.FechaVencimiento
            });
        }
    }
}