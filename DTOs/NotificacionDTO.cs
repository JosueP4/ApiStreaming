namespace APIStreaming.DTOs
{
    public class NotificacionDTO
    {
        public int Id { get; set; }

        public int? UsuariosId { get; set; }

        public string? Mensaje { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public bool? Enviado { get; set; }
    }
}
