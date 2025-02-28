using APIStreaming.DTOs;
using APIStreaming.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIStreaming.Servicios
{
    public class UsuarioServicios
    {


        private readonly ApistreamingDbContext _context;
        private readonly IConfiguration _config;

        public UsuarioServicios(ApistreamingDbContext dBContext, IConfiguration configuration)
        {
            _context = dBContext;
            _config = configuration;
        }

        
        //Registrar usuario o login
        public async Task<Usuario> LoginUser(string email, string contra)
        {
            
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Email == email && x.Contra == contra);

            return user; 
        }

        //cerrar sesion
        public async Task CerrarSesion(string refreshToken)
        {
            
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            
            if (usuario == null)
            {
                throw new Exception("Invalido o expiro el refresh token.");
            }

          
            usuario.RefreshToken = null;
            usuario.RefreshTokenExpiration = null;

            
            _context.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<UsuariosDTO> CrearUsuario(string nombre, string email, string contra)
        {
            var user = new Usuario
            {
                
                Nombre = nombre,
                Rol = "Cliente",
                Contra = contra,
                Email = email
            };

            _context.Add(user);
            await _context.SaveChangesAsync();

            return new UsuariosDTO
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Rol = user.Rol,
                Contra = user.Contra,
                Email = user.Email

            };
        }

        public async Task<UsuariosDTO> BuscarUsuario(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null) return null;

            return new UsuariosDTO
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Rol = user.Rol,
                Contra = user.Contra,
                Email = user.Email

            };

        }

        public async Task<UsuariosDTO> ActualizarUsuario(UsuariosDTO usuarioDTO, int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null) return null;


            
            user.Nombre = usuarioDTO.Nombre;
            user.Rol = usuarioDTO.Rol;
            user.Contra = usuarioDTO.Contra;
            user.Email = usuarioDTO.Email;
            

            _context.Update(user);
            await _context.SaveChangesAsync();

            return new UsuariosDTO
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Rol = user.Rol,
                Contra = user.Contra,
                Email = user.Email
            };
        }

        public async Task<UsuariosDTO> EliminarUsuario(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null) return null;


            user.EstaEliminado = true;
            await _context.SaveChangesAsync();

            return new UsuariosDTO
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Rol = user.Rol,
                Contra = user.Contra,
                Email = user.Email
            };
        }

        public async Task<UsuariosDTO> RecuperarUsuario(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null) return null;


            user.EstaEliminado = false;
            await _context.SaveChangesAsync();

            return new UsuariosDTO
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Rol = user.Rol,
                Contra = user.Contra,
                Email = user.Email
            };
        }


        //generar token y refresh Token
        public async Task<string> GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, usuario.Rol),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("UsuarioId", usuario.Id.ToString()),
                new Claim("PlanId", usuario.PlanId.ToString())


            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var security = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(security);

            return token;
                        


        }

        private string GenerarRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task<string> RefreshToken(string refreshToken)
        {
            
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            
            if (usuario == null || usuario.RefreshTokenExpiration < DateTime.Now)
            {
                throw new Exception("Refresh token no valido o expirado.");
            }

           
            var newAccessToken = await GenerarToken(usuario);

           
            var newRefreshToken = GenerarRefreshToken();
            usuario.RefreshToken = newRefreshToken;
            usuario.RefreshTokenExpiration = DateTime.Now.AddDays(5);

            
            _context.Update(usuario);
            await _context.SaveChangesAsync();

            
            return newAccessToken;
        }



    }
}
