using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaGym.API.Models
{
    public class Miembro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Telefono { get; set; }

        public string Email { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]
        public DateTime FechaVencimiento { get; set; }

        public bool Activo { get; set; } = true;

        public string HuellaId { get; set; }
    }
}