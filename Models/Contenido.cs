using System;
using System.Collections.Generic;

namespace APIStreaming.Models;

public partial class Contenido
{
    public int Id { get; set; }

    public int? SuscripcionId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Duracion { get; set; }

    public DateTime? FechaPublicacion { get; set; }

    public int? PlanId { get; set; }

    public virtual Plane? Plan { get; set; }

    public virtual Suscripcione? Suscripcion { get; set; }
}
