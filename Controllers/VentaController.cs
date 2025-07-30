using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaCazuelaChapinaAPI.Data;
using LaCazuelaChapinaAPI.Models;
using LaCazuelaChapinaAPI.Models.DTO;

namespace LaCazuelaChapinaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VentaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearVenta([FromBody] VentaRequestDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var venta = new Venta
                {
                    IdUsuarioFk = dto.IdUsuarioFk,
                    FhIngreso = dto.FhIngreso,
                    Monto = dto.Total
                };

                _context.Add(venta);
                await _context.SaveChangesAsync(); // para obtener IdVenta

                var detalles = new List<DetalleVenta>();

                foreach (var tamal in dto.Items.Tamales)
                {
                    detalles.Add(new DetalleVenta
                    {
                        IdVentaFk = venta.IdVenta,
                        IdProductoFk = tamal.IdTamal,
                        IdTipoProducto = 1, // 1 = Tamal
                        Cantidad = tamal.Cantidad,
                        Precio = tamal.Precio
                    });
                }

                foreach (var bebida in dto.Items.Bebidas)
                {
                    detalles.Add(new DetalleVenta
                    {
                        IdVentaFk = venta.IdVenta,
                        IdProductoFk = bebida.IdBebida,
                        IdTipoProducto = 2, // 2 = Bebida
                        Cantidad = bebida.Cantidad,
                        Precio = bebida.Precio
                    });
                }

                _context.AddRange(detalles);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(new { mensaje = "Venta registrada correctamente", idVenta = venta.IdVenta });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { mensaje = "Error al registrar la venta", detalle = ex.Message });
            }
        }
    }
}
