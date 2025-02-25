using System;
using System.Collections.Generic;

namespace APIStreaming.Models;

public partial class VistaContenidoConDuracion
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public int Duracion { get; set; }

    public string DuracionFormato { get; set; } = null!;
}
