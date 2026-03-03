using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGym.API.Models
{
    public class Pago
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MiembroId { get; set; }

        [ForeignKey("MiembroId")]
        public Miembro? Miembro { get; set; }

        [Required]
        public decimal Monto { get; set; }

        public DateTime FechaPago { get; set; } = DateTime.Now;

        [Required]
        public int MesesPagados { get; set; }

        public string Observaciones { get; set; }
    }
}