﻿using System;
using System.Collections.Generic;

namespace APIStreaming.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string Contra { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Rol { get; set; }

    public bool EstaEliminado { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiration { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<Suscripcione> Suscripciones { get; set; } = new List<Suscripcione>();
}
