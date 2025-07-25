using LaCazuelaChapinaAPI.Data;
using LaCazuelaChapinaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaCazuelaChapinaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BebidaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BebidaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /api/bebidas: Obtener todas las bebidas disponibles
        [HttpGet]
        public async Task<IActionResult> GetBebidas()
        {
            var bebidas = await _context.Bebidas.ToListAsync();
            return Ok(bebidas); // Devuelve la lista de bebidas
        }

        // GET /api/bebidas/{id}: Obtener una bebida específica por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBebida(int id)
        {
            var bebida = await _context.Bebidas.FindAsync(id);
            if (bebida == null)
            {
                return NotFound(); // Si no encuentra la bebida, devuelve 404
            }
            return Ok(bebida); // Devuelve la bebida encontrada
        }

        // POST /api/bebidas: Crear una nueva bebida
        [HttpPost]
        public async Task<IActionResult> CreateBebida([FromBody] Bebida bebida)
        {
            if (bebida == null)
            {
                return BadRequest("La bebida es inválida.");
            }

            _context.Bebidas.Add(bebida);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBebida), new { id = bebida.IdBebida }, bebida); // Devuelve la bebida creada
        }

        // PUT /api/bebidas/{id}: Actualizar una bebida existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBebida(int id, [FromBody] Bebida bebida)
        {
            if (id != bebida.IdBebida)
            {
                return BadRequest("El ID de la bebida no coincide.");
            }

            _context.Entry(bebida).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent(); // Devuelve un código 204 si la actualización fue exitosa
        }

        // DELETE /api/bebidas/{id}: Eliminar una bebida específica
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBebida(int id)
        {
            var bebida = await _context.Bebidas.FindAsync(id);
            if (bebida == null)
            {
                return NotFound(); // Si no se encuentra la bebida, devuelve 404
            }

            _context.Bebidas.Remove(bebida);
            await _context.SaveChangesAsync();
            return NoContent(); // Devuelve un código 204 si la bebida fue eliminada
        }
    }
}
