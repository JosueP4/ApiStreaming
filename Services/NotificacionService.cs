using Microsoft.EntityFrameworkCore;
using APIStreaming.Models;
using Microsoft.AspNetCore.Mvc;
using APIStreaming.DTOs;
namespace APIStreaming.Services
{
    public class NotificacionService
    {

        private readonly ApistreamingDbContext _context;

        public NotificacionService(ApistreamingDbContext context) { _context = context; }
             
        public async Task<List<UsuariosDTO>> UsuariosPagoPendiente()
        {
            var user = await _context.Usuarios.Where(x=> x.Suscripciones.Any(s=>s.Estado == "Activa" && s.FechaFinalizacion <=DateTime.Now.AddDays(7)&&s.Pagos.All(s=> s.Estado==false)))
                .Select(x=> new UsuariosDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Email = x.Email,
                    Contra = "******",
                    Rol = x.Rol

                    
                }).ToListAsync();

            


            return user;
        }

        public async Task CrearNotificacionPendiente()
        {
            var usuarios = await UsuariosPagoPendiente();

            foreach(var usuario in usuarios)
            {
                var notificacion = new Notificacione
                {
                    UsuariosId = usuario.Id,
                    Mensaje = "Tienes un pago pendiente",
                    FechaCreacion = DateTime.Now,
                    Enviado = false
                };
                _context.Notificaciones.Add(notificacion);
                await _context.SaveChangesAsync();
            }

        }

        public async Task EnviarNotificacionPendiente()
        {
            var notificacion = await _context.Notificaciones.Where(x => x.Enviado == false).ToListAsync();

            foreach(var notificacion1 in notificacion)
            {
                Console.WriteLine($"enviado notificacion al usuario Id: {notificacion1.UsuariosId}: {notificacion1.Mensaje}");

                notificacion1.Enviado = true;
                _context.Update(notificacion1);
            }

            await _context.SaveChangesAsync();
        }

    }
}
