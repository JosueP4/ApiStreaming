using APIStreaming.DTOs;
using APIStreaming.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIStreaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosControllers :ControllerBase
    {
        private readonly PagoServices _services;
        public PagosControllers(PagoServices services)
        {
            _services = services;
        }

        [Authorize(Roles = "Cliente, admin")]
        [HttpGet]
        [Route("HistorialPagos")]
        public async Task<ActionResult<List<PagoDTO>>> HistorialPagos([FromRoute] int id)
        {
            List<PagoDTO> pago = await _services.HistorialPagos(id);
            return Ok(pago);
        }

        [Authorize(Roles = "Cliente, admin")]
        [HttpPost]
        [Route("RealizarPagos")]
        public async Task<ActionResult> RealizarPagos([FromBody]PagoDTO pagoDTO)
        {
            PagoDTO pago = await _services.RealizarPago(pagoDTO);
            return Ok(pago);
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("BuscarPago/{id}")]
        public async Task<ActionResult> BuscarPago([FromRoute]int id)
        {
            PagoDTO pago = await _services.BuscarPago(id);
            return Ok(pago);
        }

    }
}
