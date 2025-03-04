using System;
using System.Collections.Generic;

namespace APIStreaming.Models;

public partial class Notificacione
{
    public int Id { get; set; }

    public int? UsuariosId { get; set; }

    public string? Mensaje { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Enviado { get; set; }

    public virtual Usuario? Usuarios { get; set; }
}
