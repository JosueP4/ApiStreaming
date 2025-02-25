using Microsoft.AspNetCore.Mvc;
using APIStreaming.DTOs;
using APIStreaming.Services;
using Microsoft.AspNetCore.Authorization;

namespace APIStreaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanesControllers : ControllerBase
    {

        private readonly PlaneService _Services;

        public PlanesControllers(PlaneService services)
        {
            _Services = services;
        }

        [Authorize(Roles = "Cliente, admin")]
        [HttpGet]
        [Route("ListaPlanes")]
        public async Task<ActionResult<List<PlanesDTO>>> ListaPlanes()
        {
            List <PlanesDTO >plan = await _Services.ListaPlanes();
            return Ok(plan);
        }

        [Authorize(Roles = "Cliente, admin")]
        [HttpGet]
        [Route("BuscarPlan/{id}")]
        public async Task<ActionResult> BuscarPlan([FromRoute]int id)
        {
            PlanesDTO plan = await _Services.BuscarPlan(id);
            return Ok(plan);
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("CrearPlan")]
        public async Task<ActionResult> CrearPlan([FromBody]PlanesDTO planesDTO)
        {
            PlanesDTO plan = await _Services.CrearPlan(planesDTO);
            return Ok(plan);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("ActualizarPlanes/{id}")]
        public async Task<ActionResult> ActualizarPlan([FromRoute] int id, PlanesDTO planesDTO)
        {
            PlanesDTO plan = await _Services.ActualizarPlanes(id, planesDTO);
            return Ok(plan);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("EliminarPlan/{id}")]
        public async Task<ActionResult> EliminarPlan([FromRoute]int id)
        {
            PlanesDTO plan = await _Services.EliminarPlan(id);
            return Ok();
        }

    }
}
