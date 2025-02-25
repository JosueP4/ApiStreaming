using Microsoft.AspNetCore.Mvc;
using APIStreaming.DTOs;
using APIStreaming.Servicios;
using APIStreaming.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIStreaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioServicios _service;
        


        public UsuarioController(UsuarioServicios servicio)
        {

            _service = servicio;
            
        }

        [HttpPost("IniciarSesion")]
        public async Task<ActionResult> IniciarSesion(string email, string contra)
        {
            Usuario user = await _service.LoginUser(email, contra);

            if (user == null) return NotFound("no se encontro el usuario");
            
            string JwtToken = await _service.GenerarToken(user);

            return Ok(new
            {
                token = JwtToken,
                refreshToken = user.RefreshToken
            });
        }


        [Authorize(Roles = "Cliente, admin")]
        [HttpPost("CerrarSesion")]
        public async Task<ActionResult> CerrarSesion()
        {
            var refreshToken = Request.Headers["Refresh-Token"].ToString();

            
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Refresh token es necesario.");
            }

           
            await _service.CerrarSesion(refreshToken);

            return Ok(new { message = "se cerro la sesion" });
        }

        
        [HttpPost("CrearUsuario")]
        public async Task<ActionResult> RegistratUsuario(string nombre, string email, string contra)
        {
            UsuariosDTO user = await _service.CrearUsuario(nombre, email, contra);
            return Ok(user);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("BuscarUsuario/{id}")]
        public async Task<ActionResult> BuscarUsuario([FromRoute]int id)
        {

            UsuariosDTO user = await _service.BuscarUsuario(id);
            return Ok(user);

        }


        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("ActualizarUsuario/{id}")]
        public async Task<ActionResult> ActualizarUsuario([FromBody]UsuariosDTO usuariosDTO, int id)
        {

            UsuariosDTO user = await _service.ActualizarUsuario(usuariosDTO, id);
            return Ok(user);

        }


        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("EliminarUsuario/{id}")]
        public async Task<ActionResult> EliminarUsuario([FromRoute]int id)
        {

            UsuariosDTO user = await _service.EliminarUsuario(id);
            return Ok();

        }

        [Authorize(Roles ="admin")]
        [HttpPut]
        [Route("RecuperarUsuario/{id}")]
        public async Task<ActionResult> RecuperarUsuario([FromRoute] int id)
        {

            UsuariosDTO user = await _service.RecuperarUsuario(id);
            return Ok();

        }
        



    }
}
