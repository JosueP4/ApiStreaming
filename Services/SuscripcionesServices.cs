using APIStreaming.DTOs;
using APIStreaming.Models;


namespace APIStreaming.Services
{
    public class SuscripcionesServices
    {


        private readonly ApistreamingDbContext _context;

        public SuscripcionesServices(ApistreamingDbContext context)
        {
            _context = context;
        }


        public async Task<SuscripcionesDTO> CrearSuscripciones(SuscripcionesDTO suscripcionesDTO)
        {
            var suscripcion = new Suscripcione
            {
                Id = suscripcionesDTO.Id,
                UsuarioId = suscripcionesDTO.UsuarioId,
                FechaFinalizacion = suscripcionesDTO.FechaFinalizacion,
                FechaInicio = suscripcionesDTO.FechaInicio,
                Estado = suscripcionesDTO.Estado,
                PlanId = suscripcionesDTO.PlanId

            };

            _context.Add(suscripcion);
            await _context.SaveChangesAsync();

            return new SuscripcionesDTO 
            { 
            
            Id = suscripcion.Id,
            UsuarioId = suscripcion.UsuarioId,
            FechaFinalizacion = suscripcion.FechaFinalizacion,
            FechaInicio = suscripcion.FechaInicio,
            Estado = suscripcion.Estado,
            PlanId = suscripcion.PlanId


            };


        }

        public async Task<SuscripcionesDTO> BuscarSuscripcion(int id)
        {
            var suscripcion = await _context.Suscripciones.FindAsync(id);
            if (suscripcion == null) return null;

            return new SuscripcionesDTO
            { 
                Id = suscripcion.Id,
                UsuarioId = suscripcion.UsuarioId,
                FechaFinalizacion = suscripcion.FechaFinalizacion,
                FechaInicio = suscripcion.FechaInicio,
                Estado = suscripcion.Estado,
                PlanId = suscripcion.PlanId
            };

        }

        public async Task<SuscripcionesDTO> CancelarSuscripciones(int id)
        {
            var suscripcion = await _context.Suscripciones.FindAsync(id);
            if (suscripcion == null) return null;


            suscripcion.Estado = "cancelada";
            _context.Update(suscripcion);
            await _context.SaveChangesAsync();

            return new SuscripcionesDTO
            {
                Id = suscripcion.Id,
                UsuarioId = suscripcion.UsuarioId,
                FechaFinalizacion = suscripcion.FechaFinalizacion,
                FechaInicio = suscripcion.FechaInicio,
                Estado = suscripcion.Estado,
                PlanId = suscripcion.PlanId
            };


        }


    }
}
