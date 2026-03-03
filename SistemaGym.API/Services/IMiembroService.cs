using SistemaGym.API.Models;
using SistemaGym.API.Models.DTOs;

namespace SistemaGym.API.Services
{
    public interface IMiembroService
    {
        Task<List<Miembro>> ObtenerTodos();
        Task<Miembro?> ObtenerPorId(int id);
        Task<Miembro> Crear(Miembro miembro);
        Task<(bool permitido, string mensaje)> ValidarAcceso(int id);
        Task<List<Acceso>> ObtenerAccesos(int id);

        // 👇 ESTE ES EL QUE TE FALTA
        Task<(bool exito, string mensaje, DateTime? nuevaFecha)>
            RegistrarPago(int id, PagoRequest request);
    }
}