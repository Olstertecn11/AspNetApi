using LaCazuelaChapinaAPI.Data;
using LaCazuelaChapinaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaCazuelaChapinaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TamalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TamalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /api/tamales: Obtener todos los tamales disponibles
        [HttpGet]
        public async Task<IActionResult> GetTamales()
        {
            var tamales = await _context.Tamales
                .Include(t => t.TipoMasa) // Incluye las relaciones de las claves foráneas
                .Include(t => t.Relleno)
                .Include(t => t.Envoltura)
                .Include(t => t.NivelPicante)
                .ToListAsync();

            if (tamales == null || tamales.Count == 0)
            {
                return NotFound("No se encontraron tamales.");
            }

            return Ok(tamales); // Devuelve la lista de tamales
        }

        // GET /api/tamales/{id}: Obtener un tamal específico por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTamal(int id)
        {
            var tamal = await _context.Tamales
                .Include(t => t.TipoMasa) // Incluye las relaciones de las claves foráneas
                .Include(t => t.Relleno)
                .Include(t => t.Envoltura)
                .Include(t => t.NivelPicante)
                .FirstOrDefaultAsync(t => t.IdTamal == id);

            if (tamal == null)
            {
                return NotFound($"No se encontró el tamal con ID {id}.");
            }

            return Ok(tamal); // Devuelve el tamal encontrado
        }

        // POST /api/tamales: Crear un nuevo tamal
        [HttpPost]
        public async Task<IActionResult> CreateTamal([FromBody] Tamal tamal)
        {
            if (tamal == null)
            {
                return BadRequest("El tamal no es válido.");
            }

            _context.Tamales.Add(tamal);
            await _context.SaveChangesAsync(); // Guarda el nuevo tamal en la base de datos

            return CreatedAtAction(nameof(GetTamal), new { id = tamal.IdTamal }, tamal); // Devuelve el tamal creado
        }

        // PUT /api/tamales/{id}: Actualizar un tamal existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTamal(int id, [FromBody] Tamal tamal)
        {
            if (id != tamal.IdTamal)
            {
                return BadRequest("El ID del tamal no coincide.");
            }

            var existingTamal = await _context.Tamales.FindAsync(id);
            if (existingTamal == null)
            {
                return NotFound($"No se encontró el tamal con ID {id}.");
            }

            // Actualiza las propiedades del tamal
            existingTamal.Precio = tamal.Precio;
            existingTamal.Inventario = tamal.Inventario;
            existingTamal.IdTipoMasaFk = tamal.IdTipoMasaFk;
            existingTamal.IdRellenoFk = tamal.IdRellenoFk;
            existingTamal.IdEnvolturaFk = tamal.IdEnvolturaFk;
            existingTamal.IdNivelPicante = tamal.IdNivelPicante;

            _context.Entry(existingTamal).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve un código 204 si la actualización fue exitosa
        }

        // DELETE /api/tamales/{id}: Eliminar un tamal específico
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTamal(int id)
        {
            var tamal = await _context.Tamales.FindAsync(id);
            if (tamal == null)
            {
                return NotFound($"No se encontró el tamal con ID {id}.");
            }

            _context.Tamales.Remove(tamal);
            await _context.SaveChangesAsync(); // Elimina el tamal de la base de datos

            return NoContent(); // Devuelve un código 204 si el tamal fue eliminado
        }
    }
}
