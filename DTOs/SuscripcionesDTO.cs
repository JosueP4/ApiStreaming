namespace APIStreaming.DTOs
{
    public class SuscripcionesDTO
    {

        public int Id { get; set; }

        public int? UsuarioId { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFinalizacion { get; set; }

        public string Estado { get; set; } = null!;

        public int? PlanId { get; set; }

    }
}
