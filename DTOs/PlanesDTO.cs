namespace APIStreaming.DTOs
{
    public class PlanesDTO
    {
        public int Id { get; set; }

        public string NombrePlan { get; set; } = null!;

        public decimal Precio { get; set; }

        public int Resolucion { get; set; }

        public string Exclusividad { get; set; } = null!;

        public string Descargas { get; set; } = null!;
    }
}
