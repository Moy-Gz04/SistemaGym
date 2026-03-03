using Microsoft.EntityFrameworkCore;
using SistemaGym.API.Data;
using SistemaGym.API.Models;

namespace SistemaGym.API.Services
{
    public class MiembroService : IMiembroService
    {
        private readonly GymDbContext _context;

        public MiembroService(GymDbContext context)
        {
            _context = context;
        }

        public async Task<List<Miembro>> ObtenerTodos()
        {
            return await _context.Miembros.ToListAsync();
        }

        public async Task<Miembro?> ObtenerPorId(int id)
        {
            return await _context.Miembros.FindAsync(id);
        }

        public async Task<Miembro> Crear(Miembro miembro)
        {
            _context.Miembros.Add(miembro);
            await _context.SaveChangesAsync();
            return miembro;
        }

        public async Task<(bool permitido, string mensaje)> ValidarAcceso(int id)
        {
            var miembro = await _context.Miembros.FindAsync(id);

            if (miembro == null)
                return (false, "Miembro no encontrado");

            if (!miembro.Activo)
                return (false, "Membresía inactiva");

            if (miembro.FechaVencimiento < DateTime.Now)
                return (false, "Membresía vencida");

            return (true, "Acceso permitido");
        }
    }
}