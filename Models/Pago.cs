using System;
using System.Collections.Generic;

namespace APIStreaming.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int? SuscripcionId { get; set; }

    public int? UsuarioId { get; set; }

    public decimal Monto { get; set; }

    public string? MetodoPago { get; set; }

    public DateTime? FechaPago { get; set; }

    public bool? Estado { get; set; }

    public virtual Suscripcione? Suscripcion { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
