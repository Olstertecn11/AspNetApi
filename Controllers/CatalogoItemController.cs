using LaCazuelaChapinaAPI.Data;
using LaCazuelaChapinaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaCazuelaChapinaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CatalogoItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /api/catalogoitem: Obtener todos los items del catálogo
        [HttpGet]
        public async Task<IActionResult> GetCatalogoItems()
        {
            var catalogoItems = await _context.CatalogoItems.Include(c => c.Catalogo).ToListAsync();

            if (catalogoItems == null || catalogoItems.Count == 0)
            {
                return NotFound("No se encontraron items de catálogo.");
            }

            return Ok(catalogoItems);
        }

        // GET /api/catalogoitem/{id}: Obtener un item de catálogo específico por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogoItem(int id)
        {
            var catalogoItem = await _context.CatalogoItems
                .Include(c => c.Catalogo)
                .FirstOrDefaultAsync(c => c.IdCatalogoItem == id);

            if (catalogoItem == null)
            {
                return NotFound("Item de catálogo no encontrado.");
            }

            return Ok(catalogoItem);
        }

        // POST /api/catalogoitem: Crear un nuevo item de catálogo
        [HttpPost]
        public async Task<IActionResult> CreateCatalogoItem([FromBody] CatalogoItem catalogoItem)
        {
            if (catalogoItem == null)
            {
                return BadRequest("Los datos del item de catálogo son incorrectos.");
            }

            _context.CatalogoItems.Add(catalogoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCatalogoItem), new { id = catalogoItem.IdCatalogoItem }, catalogoItem);
        }

        // PUT /api/catalogoitem/{id}: Actualizar un item de catálogo existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCatalogoItem(int id, [FromBody] CatalogoItem catalogoItem)
        {
            if (id != catalogoItem.IdCatalogoItem)
            {
                return BadRequest("El ID del item de catálogo no coincide.");
            }

            _context.Entry(catalogoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent(); // HTTP 204 (sin contenido)
        }

        // DELETE /api/catalogoitem/{id}: Eliminar un item de catálogo específico
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogoItem(int id)
        {
            var catalogoItem = await _context.CatalogoItems.FindAsync(id);
            if (catalogoItem == null)
            {
                return NotFound("Item de catálogo no encontrado.");
            }

            _context.CatalogoItems.Remove(catalogoItem);
            await _context.SaveChangesAsync();

            return NoContent(); // HTTP 204 (sin contenido)
        }
    }
}
