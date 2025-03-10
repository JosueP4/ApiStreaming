using APIStreaming.DTOs;
using APIStreaming.Models;
using APIStreaming.Services;
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
        // Endpoint para obtener usuarios con pagos pendientes
        [HttpGet("usuarios-pago-pendiente")]
        public async Task<ActionResult<List<UsuariosDTO>>> GetUsuariosPagoPendiente()
        {
            var usuarios = await _services.UsuariosPagoPendiente();
            return Ok(usuarios);
        }

        // Endpoint para crear notificaciones pendientes
        [HttpPost("crear-notificaciones-pendientes")]
        public async Task<IActionResult> CrearNotificacionesPendientes()
        {
            await _services.CrearNotificacionPendiente();
            return Ok("Notificaciones pendientes creadas correctamente.");
        }

        // Endpoint para enviar notificaciones pendientes
        [HttpPost("enviar-notificaciones-pendientes")]
        public async Task<IActionResult> EnviarNotificacionesPendientes()
        {
            await _services.EnviarNotificacionPendiente();
            return Ok("Notificaciones pendientes enviadas correctamente.");
        }

    }
}
