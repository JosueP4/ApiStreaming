using APIStreaming.DTOs;
using APIStreaming.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APIStreaming.Services
{
    public class ContenidoServices
    {

        private readonly ApistreamingDbContext _context;

        public ContenidoServices(ApistreamingDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContenidoDTO>> ListaVideos()
        {

            List<ContenidoDTO> contenidoList = new List<ContenidoDTO>();
            var contenido = await _context.Contenidos.ToListAsync();

            foreach (var item in contenidoList)
            {
                var contenido1 = new ContenidoDTO
                {
                    Id = item.Id,
                    SuscripcionId = item.SuscripcionId,
                    Descripcion = item.Descripcion,
                    Titulo = item.Titulo,
                    Duracion = item.Duracion,
                    FechaPublicacion = item.FechaPublicacion,
                    Acceso = item.Acceso

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
                Acceso = contenido.Acceso
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
                Acceso = contenidoDTO.Acceso


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
                Acceso = contenido.Acceso

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
            contenido.Acceso = contenido.Acceso;

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
                Acceso = contenido.Acceso


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
                Acceso = contenido.Acceso
            };

        }


    }
}
