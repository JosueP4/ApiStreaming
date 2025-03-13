using APIStreaming.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace APIStreaming.Services
{
    public class BackGroundUsuario : BackgroundService
    {


        private readonly IServiceScopeFactory _factory;
        private readonly ILogger<BackGroundUsuario> _logger;


        public BackGroundUsuario(IServiceScopeFactory factory, ILogger<BackGroundUsuario> logger)
        {
            _factory = factory;
            _logger = logger;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope =  _factory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApistreamingDbContext>();

                    var userVencidos = await context.Usuarios.Include(x => x.Suscripciones)
                        .Where(x => x.Suscripciones.Any())
                        .Where(x => x.Suscripciones.OrderByDescending(x => x.FechaFinalizacion).FirstOrDefault().FechaFinalizacion <= DateTime.Now.AddDays(-5) && !x.EstaEliminado).ToListAsync();
                        
                    foreach(var user in userVencidos)
                    {
                        user.EstaEliminado = true;
                        
                    }

                    await context.SaveChangesAsync();

                    _logger.LogInformation("El backGround de verificar que los usuarios tienen la suscripcion esta vencidad");
                    _logger.LogInformation($"El backGround de verificar que los usuarios a borrado{userVencidos.Count}");

                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }

            

        }

    }
}
