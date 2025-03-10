using System;
using System.Collections.Generic;

namespace APIStreaming.Models;

public partial class Suscripcione
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime? FechaInicio { get; set; }

    public string Estado { get; set; } = null!;

    public int? PlanId { get; set; }

    public DateTime? FechaFinalizacion { get; set; }

    public virtual ICollection<Contenido> Contenidos { get; set; } = new List<Contenido>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual Plane? Plan { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
