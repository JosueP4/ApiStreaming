using APIStreaming.DTOs;
using APIStreaming.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;

namespace APIStreaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContenidoControllers :ControllerBase
    {

        private readonly ContenidoServices _services;

        public ContenidoControllers(ContenidoServices services)
        {
            _services = services;
        }



        [Authorize(Roles = "Cliente, admin")]
        [HttpGet("ListaContenido")]
        public async Task<ActionResult<List<ContenidoDTO>>> ListaContenido()
        {
            List<ContenidoDTO> contenido = await _services.ListaVideos(User);
            return Ok(contenido);
        }



        [Authorize(Roles = "Cliente, admin")]
        [HttpGet("BuscarContenido/{id}")]
        public async Task<ActionResult> BuscarContenido(int id)
        {
            ContenidoDTO contenido = await _services.BuscarContenido(id);
            return Ok(contenido);
        }


        [Authorize(Roles = "admin")]
        [HttpPost("CrearContenido")]
        public async Task<ActionResult> CrearContenido([FromBody]ContenidoDTO contenidoDTO)
        {
            ContenidoDTO contenido = await _services.SubirContenido(contenidoDTO);
            return Ok(contenido);
        }


        [Authorize(Roles = "admin")]
        [HttpPut("EditarContenido/{id}")]
        public async Task<ActionResult> EditarContenido(ContenidoDTO contenidoDTO, int id)
        {
            ContenidoDTO contenido = await _services.ActualizarContenido(contenidoDTO, id);
            return Ok(contenido);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("EliminarContenido/{id}")]
        public async Task<ActionResult> EliminarContenido(int id)
        {
            ContenidoDTO contenido = await _services.EliminarContenido(id);
            return Ok();
        }

    }
}
