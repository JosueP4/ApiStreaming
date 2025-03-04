using APIStreaming.Models;
using APIStreaming.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIStreaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {
        private readonly NotificacionService _services;
        public NotificacionesController(NotificacionService service)
        {
            _services = service;
        }

        [HttpGet("Pagospendientes")]
        public async Task<ActionResult<List<Notificacione>>> ObtenerNotificacionesPendientes()
        {
            var notificaciones = await _services.ListaNotificacionesPendiente();
            return Ok(notificaciones);
        }

        [HttpPut("EnviarNotificacion/{id}")]
        public async Task<ActionResult> EnviarNotificaciones(int id)
        {
            var notificaciones = await _services.EnviarNotificacion(id);
            return Ok(notificaciones);
        }


    }
}
