using APIStreaming.Models;
using Microsoft.EntityFrameworkCore;

namespace APIStreaming.Services
{
    public class BackGroundNotificaciones : BackgroundService
    {

        private readonly Logger<NotificacionService> _logger;
        private readonly IServiceScopeFactory _factory;

        public BackGroundNotificaciones(Logger<NotificacionService> logger, IServiceScopeFactory service)
        {
            _logger = logger;
            _factory = service;
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                using(var scope = _factory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApistreamingDbContext>();

                    var deudores = await context.Usuarios.Include(x => x.Suscripciones)
                        .Where(x => x.Suscripciones.Any())
                        .Where(x => x.Suscripciones.OrderByDescending(x => x.FechaFinalizacion).FirstOrDefault().FechaFinalizacion <= DateTime.Now.AddDays(-5) && !x.EstaEliminado).ToListAsync();



                   foreach(var notificacion in deudores)
                    {
                        var notificaciones = new Notificacione
                        {
                            UsuariosId = notificacion.Id,
                            Mensaje = "El usuario tiene un plazo de 5 dias para pagar la suscripcion o de lo contrario sera cerrado",
                            FechaCreacion = DateTime.Now,
                            Enviado = true

                        };

                        context.Notificaciones.Add(notificaciones);
                        await context.SaveChangesAsync();
                    }

                    
                   

                    _logger.LogInformation("Este background sirve para poder enviar los mensajes");


                }
            }
        }


    }
}
