using APIStreaming.DTOs;
using APIStreaming.Models;
using Microsoft.EntityFrameworkCore;

namespace APIStreaming.Services
{
    public class PagoServices
    {
        private readonly ApistreamingDbContext _context;
        
        public PagoServices(ApistreamingDbContext context) { _context = context; }

        public async Task<List<PagoDTO>> HistorialPagos(int id)
        {

            List<PagoDTO> pagolist = new List<PagoDTO>();
            
            var user = await _context.Usuarios.FindAsync(id);
            var pagos = await _context.Pagos.ToListAsync();
            
            if (user == null) return null;

            foreach(var item in pagolist)
            {
                var pago1 = new PagoDTO
                {
                    Id = item.Id,
                    SuscripcionId = item.SuscripcionId,
                    Monto = item.Monto,
                    MetodoPago = item.MetodoPago,
                    FechaPago = item.FechaPago,
                    Estado = item.Estado
                };

                pagolist.Add(pago1);
            }

            return pagolist;

        }

        public async Task<PagoDTO> RealizarPago(PagoDTO pagoDTO)
        {
            var pago = new Pago
            {
                Id = pagoDTO.Id,
                SuscripcionId = pagoDTO.SuscripcionId,
                Monto = pagoDTO.Monto,
                MetodoPago = pagoDTO.MetodoPago,
                FechaPago = pagoDTO.FechaPago,
                Estado = pagoDTO.Estado
            };

            _context.Add(pago);
            await _context.SaveChangesAsync();

            return new PagoDTO
            {
                Id = pago.Id,
                SuscripcionId = pago.SuscripcionId,
                Monto = pago.Monto,
                MetodoPago = pago.MetodoPago,
                FechaPago = pago.FechaPago,
                Estado = pago.Estado
            };
        }

        public async Task<PagoDTO> BuscarPago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null) return null;


            return new PagoDTO
            {
                Id = pago.Id,
                SuscripcionId = pago.SuscripcionId,
                Monto = pago.Monto,
                MetodoPago = pago.MetodoPago,
                FechaPago = pago.FechaPago,
                Estado = pago.Estado
            };


        }

        //Futuro endpoints para la notificacion cuando se realize el pago
    }
}
