using APIStreaming.DTOs;
using APIStreaming.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace APIStreaming.Services
{
    public class ContenidoServices
    {

        private readonly ApistreamingDbContext _context;

        public ContenidoServices(ApistreamingDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContenidoDTO>> ListaVideos(ClaimsPrincipal user)
        {
            var usuarioClaim = user.FindFirst("UsuarioId");
            if (usuarioClaim == null) throw new Exception("Usuario no encontrado");

            var planClaim = user.FindFirst("PlanId");
            if (planClaim == null) throw new Exception("Plan no encontrado");

            if (!int.TryParse(planClaim.Value, out int userPlanId))
                throw new Exception("PlanId inválido");

            IQueryable<Contenido> query = _context.Contenidos;

            if (userPlanId == 5)
                query = query.Where(c => c.PlanId == 5);
            else if (userPlanId == 6)
                query = query.Where(c => c.PlanId == 5 || c.PlanId == 6);
            

            List<ContenidoDTO> contenidoList = new List<ContenidoDTO>();

            foreach (var contenido in query)
            {
                var contenido1 = new ContenidoDTO
                {
                    Id = contenido.Id,
                    SuscripcionId = contenido.SuscripcionId,
                    Descripcion = contenido.Descripcion,
                    Titulo = contenido.Titulo,
                    Duracion = contenido.Duracion,
                    FechaPublicacion = contenido.FechaPublicacion,
                    PlanId = contenido.PlanId
                };

                
            contenidoList.Add(contenido1);
            }


            return contenidoList;
        }

        public async Task<ContenidoDTO> BuscarContenido(int id)
        {
            var contenido = await _context.Contenidos.FindAsync(id);
            if (contenido == null) return null;

            return new ContenidoDTO
            {
                Id = contenido.Id,
                SuscripcionId = contenido.SuscripcionId,
                Descripcion = contenido.Descripcion,
                Titulo = contenido.Titulo,
                Duracion = contenido.Duracion,
                FechaPublicacion = contenido.FechaPublicacion,
                PlanId = contenido.PlanId
            };
        }

        public async Task<ContenidoDTO> SubirContenido(ContenidoDTO contenidoDTO)
        {


            var contenido = new Contenido
            {
                Id = contenidoDTO.Id,
                SuscripcionId = contenidoDTO.SuscripcionId,
                Descripcion = contenidoDTO.Descripcion,
                Titulo = contenidoDTO.Titulo,
                Duracion = contenidoDTO.Duracion,
                FechaPublicacion = DateTime.Now,
                PlanId = contenidoDTO.PlanId


            };

            _context.Add(contenido);
            await _context.SaveChangesAsync();

            return new ContenidoDTO
            {
                Id = contenido.Id,
                SuscripcionId = contenido.SuscripcionId,
                Descripcion = contenido.Descripcion,
                Titulo = contenido.Titulo,
                Duracion = contenido.Duracion,
                FechaPublicacion = contenido.FechaPublicacion,
                PlanId = contenido.PlanId

            };
        }

        public async Task<ContenidoDTO> ActualizarContenido(ContenidoDTO contenidoDTO, int id)
        {
            var contenido = await _context.Contenidos.FindAsync(id);
            if (contenido == null) return null;



            contenido.Descripcion = contenidoDTO.Descripcion;
            contenido.Titulo = contenidoDTO.Titulo;
            contenido.Titulo = contenidoDTO.Titulo;
            contenido.Duracion = contenido.Duracion;
            contenido.FechaPublicacion = contenido.FechaPublicacion;
            contenido.PlanId = contenido.PlanId;

            _context.Update(contenido);
            await _context.SaveChangesAsync();

            return new ContenidoDTO
            {
                Id = contenido.Id,
                SuscripcionId = contenido.SuscripcionId,
                Descripcion = contenido.Descripcion,
                Titulo = contenido.Titulo,
                Duracion = contenido.Duracion,
                FechaPublicacion = contenido.FechaPublicacion,
                PlanId = contenido.PlanId


            };

        }

        public async Task<ContenidoDTO> EliminarContenido(int id)
        {
            var contenido = await _context.Contenidos.FindAsync(id);
            if (contenido == null) return null;

            _context.Remove(contenido);
            await _context.SaveChangesAsync();

            return new ContenidoDTO
            {
                Id = contenido.Id,
                SuscripcionId = contenido.SuscripcionId,
                Descripcion = contenido.Descripcion,
                Titulo = contenido.Titulo,
                Duracion = contenido.Duracion,
                FechaPublicacion = contenido.FechaPublicacion,
                PlanId = contenido.PlanId
            };

        }


    }
}
