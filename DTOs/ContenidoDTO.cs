namespace APIStreaming.DTOs
{
    public class ContenidoDTO
    {
        public int Id { get; set; }

        public int? SuscripcionId { get; set; }

        public string Titulo { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public int Duracion { get; set; }

        public DateTime? FechaPublicacion { get; set; }
        public int? PlanId { get; set; }
        public string? DuracionFormato { get; set; }






    }
}
