using APIStreaming.DTOs;
using APIStreaming.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace APIStreaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuscripcionController : ControllerBase
    {

        private readonly SuscripcionesServices _services;

        public SuscripcionController(SuscripcionesServices services)
        {
            _services = services;
        }


        [Authorize(Roles = "admin, Cliente")]
        [HttpPost]
        [Route("CrearSuscripcion")]
        public async Task<ActionResult> CrearSuscripcion([FromBody] SuscripcionesDTO suscripcionesDTO)
        {
            SuscripcionesDTO suscripcion = await _services.CrearSuscripciones(suscripcionesDTO);
            return Ok(suscripcion);
        }

        [Authorize(Roles = "admin")]
        [Authorize]
        [HttpGet]
        [Route("BuscarSuscripcion/{id}")]
        public async Task<ActionResult> BuscarSuscripcion([FromRoute]int id)
        {
            SuscripcionesDTO suscripcion = await _services.BuscarSuscripcion(id);
            return Ok(suscripcion);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("CancelarSuscripcion/{id}")]
        public async Task<ActionResult> CancelarSuscripcion(int id)
        {
            SuscripcionesDTO suscripcion = await _services.CancelarSuscripciones(id);
            return Ok(suscripcion);
        }

    }
}
