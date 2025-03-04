namespace APIStreaming.DTOs
{
    public class PagoDTO
    {
        public int Id { get; set; }

        public int? SuscripcionId { get; set; }

        public int? UsuarioId { get; set; }

        public decimal Monto { get; set; }

        public string? MetodoPago { get; set; }

        public DateTime? FechaPago { get; set; }

        public bool? Estado { get; set; }

    }
}
