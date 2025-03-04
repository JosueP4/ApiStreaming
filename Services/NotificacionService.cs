using Microsoft.EntityFrameworkCore;
using APIStreaming.Models;
using Microsoft.AspNetCore.Mvc;
namespace APIStreaming.Services
{
    public class NotificacionService
    {

        private readonly ApistreamingDbContext _context;
        private readonly ILogger _looger;

        public NotificacionService(ApistreamingDbContext context, ILogger logger) { _context = context; _looger = logger; }
        public async Task RecordarPago()
        {
            var pagoPendiente = await _context.Pagos
                .Where(x => x.Estado == false )
                .ToListAsync();

            _looger.LogInformation($"Pagos pendientes encontrados: {pagoPendiente.Count}");
            foreach (var pago in pagoPendiente)
            {
                var suscripciones = await _context.Suscripciones
                    .Include(x => x.Usuario)
                    .FirstOrDefaultAsync(x => x.Id == pago.SuscripcionId);

                if (suscripciones == null || suscripciones.Usuario == null)
                {
                    // Si no se encuentra la suscripción o usuario, omite la notificación
                    continue;
                }

                var user = suscripciones.Usuario;

                var notificacion = new Notificacione
                {
                    UsuariosId = user.Id,
                    Mensaje = $"{user.Nombre} es para recordarle realizar el pago de la fecha {pago.FechaPago}",
                    FechaCreacion = DateTime.Now,
                    Enviado = false
                };

                _context.Add(notificacion); // Agrega la notificación a la lista
            }

            await _context.SaveChangesAsync(); // Guardar todas las notificaciones al final
        }


        public async Task<List<Notificacione>> ListaNotificacionesPendiente()
        {
            return await _context.Notificaciones.Where(x => x.Enviado == false).ToListAsync();
        }


        public async Task<Notificacione> EnviarNotificacion(int id)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null) return null;

            notificacion.Enviado = true;
            await _context.SaveChangesAsync();

            return notificacion;
        }


    }
}
