using APIStreaming.DTOs;
using APIStreaming.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APIStreaming.Services
{
    public class PlaneService
    {

        private readonly ApistreamingDbContext _context;

        public PlaneService(ApistreamingDbContext context)
        {

            _context = context;

        }

        public async Task<List<PlanesDTO>> ListaPlanes()
        {
            List<PlanesDTO> planList = new List<PlanesDTO>();

            var plan = await _context.Planes.ToListAsync();

            foreach (var item in plan)
            {
                var plan1 = new PlanesDTO
                {

                    Id = item.Id,
                    NombrePlan = item.NombrePlan,
                    Precio = item.Precio,
                    Resolucion = item.Resolucion,
                    Exclusividad = item.Exclusividad,
                    Descargas = item.Descargas


                };

                planList.Add(plan1);

            }
            return planList;

        }

        public async Task<PlanesDTO> BuscarPlan(int id)
        {
            var planes = await _context.Planes.FindAsync(id);
            if (planes == null) return null;

            return new PlanesDTO
            {

                Id = planes.Id,
                NombrePlan = planes.NombrePlan,
                Precio = planes.Precio,
                Resolucion = planes.Resolucion,
                Exclusividad = planes.Exclusividad,
                Descargas = planes.Descargas


            };

        }

        public async Task<PlanesDTO> CrearPlan(PlanesDTO planesDTO)
        {

            var planes = new Plane
            {
                Id = planesDTO.Id,
                NombrePlan = planesDTO.NombrePlan,
                Precio = planesDTO.Precio,
                Resolucion = planesDTO.Resolucion,
                Exclusividad = planesDTO.Exclusividad,
                Descargas = planesDTO.Descargas
            };

            _context.Add(planes);
            await _context.SaveChangesAsync();

            return new PlanesDTO
            {
                Id = planes.Id,
                NombrePlan = planes.NombrePlan,
                Precio = planes.Precio,
                Resolucion = planes.Resolucion,
                Exclusividad = planes.Exclusividad,
                Descargas = planes.Descargas
            };


        }


        public async Task<PlanesDTO> ActualizarPlanes(int id, PlanesDTO planesDTO)
        {
            var planes = await _context.Planes.FindAsync(id);
            if (planes == null) return null;



            planes.NombrePlan = planesDTO.NombrePlan;
            planes.Precio = planesDTO.Precio;
            planes.Resolucion = planesDTO.Resolucion;
            planes.Exclusividad = planesDTO.Exclusividad;
            planes.Descargas = planesDTO.Descargas;

            _context.Update(planes);
            await _context.SaveChangesAsync();

            return new PlanesDTO
            {
                Id = planes.Id,
                NombrePlan = planes.NombrePlan,
                Precio = planes.Precio,
                Resolucion = planes.Resolucion,
                Exclusividad = planes.Exclusividad,
                Descargas = planes.Descargas
            };

        }

        public async Task<PlanesDTO> EliminarPlan(int id)
        {
            var planes = await _context.Planes.FindAsync(id);
            if (planes == null) return null;

            _context.Remove(planes);
            await _context.SaveChangesAsync();

            return new PlanesDTO
            {
                Id = planes.Id,
                NombrePlan = planes.NombrePlan,
                Precio = planes.Precio,
                Resolucion = planes.Resolucion,
                Exclusividad = planes.Exclusividad,
                Descargas = planes.Descargas
            };
        }
    }
}
