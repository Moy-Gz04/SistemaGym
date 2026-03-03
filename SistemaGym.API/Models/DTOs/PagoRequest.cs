namespace SistemaGym.API.Models.DTOs
{
    public class PagoRequest
    {
        public decimal Monto { get; set; }
        public int MesesPagados { get; set; }
        public string Observaciones { get; set; }
    }
}