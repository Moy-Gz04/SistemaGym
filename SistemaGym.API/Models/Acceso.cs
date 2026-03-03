using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGym.API.Models
{
    public class Acceso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MiembroId { get; set; }

        [ForeignKey("MiembroId")]
        public Miembro Miembro { get; set; }

        public DateTime FechaHora { get; set; } = DateTime.Now;

        public bool AccesoPermitido { get; set; }

        public string Mensaje { get; set; }
    }
}