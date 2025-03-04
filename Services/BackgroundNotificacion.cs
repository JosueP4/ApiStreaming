namespace APIStreaming.Services
{
    public class BackgroundNotificacion : BackgroundService
    {

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<BackgroundNotificacion> _logger;


        public BackgroundNotificacion(IServiceScopeFactory scopeFactory, ILogger<BackgroundNotificacion> logger) 
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using(var scope = _scopeFactory.CreateScope())
                {
                    var notificacionServices = scope.ServiceProvider.GetRequiredService<NotificacionService>();
                    try
                    {
                        await notificacionServices.RecordarPago();
                        _logger.LogInformation("Notificaciones generadas correctamente");
                    }catch(Exception ex)
                    {
                        _logger.LogError(ex, "error al generar la notificacion");

                    }
                }

                await Task.Delay(TimeSpan.FromHours(1), cancellationToken);
            }
        }

    }
}
