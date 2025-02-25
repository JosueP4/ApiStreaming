namespace APIStreaming.DTOs
{
    public class UsuariosDTO
    {

        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string Contra { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Rol { get; set; }


    }
}
