using System;
using System.Collections.Generic;

namespace APIStreaming.Models;

public partial class Plane
{
    public int Id { get; set; }

    public string NombrePlan { get; set; } = null!;

    public decimal Precio { get; set; }

    public int Resolucion { get; set; }

    public string Exclusividad { get; set; } = null!;

    public string Descargas { get; set; } = null!;

    public virtual ICollection<Suscripcione> Suscripciones { get; set; } = new List<Suscripcione>();
}
