using LaCazuelaChapinaAPI.Data;
using LaCazuelaChapinaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaCazuelaChapinaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CatalogoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /api/catalogo: Obtener todos los catálogos
        [HttpGet]
        public async Task<IActionResult> GetCatalogos()
        {
            var catalogos = await _context.Catalogos.ToListAsync();

            if (catalogos == null || catalogos.Count == 0)
            {
                return NotFound("No se encontraron catálogos.");
            }

            return Ok(catalogos);
        }

        // GET /api/catalogo/{id}: Obtener un catálogo específico por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogo(int id)
        {
            var catalogo = await _context.Catalogos.FindAsync(id);

            if (catalogo == null)
            {
                return NotFound("Catálogo no encontrado.");
            }

            return Ok(catalogo);
        }

        // POST /api/catalogo: Crear un nuevo catálogo
        [HttpPost]
        public async Task<IActionResult> CreateCatalogo([FromBody] Catalogo catalogo)
        {
            if (catalogo == null)
            {
                return BadRequest("Los datos del catálogo son incorrectos.");
            }

            // Validar que el nombre y la descripción estén presentes
            if (string.IsNullOrEmpty(catalogo.Nombre) || string.IsNullOrEmpty(catalogo.Descripcion))
            {
                return BadRequest("El nombre y la descripción son obligatorios.");
            }

            _context.Catalogos.Add(catalogo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCatalogo), new { id = catalogo.IdCatalogo }, catalogo);
        }

        // PUT /api/catalogo/{id}: Actualizar un catálogo existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCatalogo(int id, [FromBody] Catalogo catalogo)
        {
            if (id != catalogo.IdCatalogo)
            {
                return BadRequest("El ID del catálogo no coincide.");
            }

            // Validar que el nombre y la descripción estén presentes
            if (string.IsNullOrEmpty(catalogo.Nombre) || string.IsNullOrEmpty(catalogo.Descripcion))
            {
                return BadRequest("El nombre y la descripción son obligatorios.");
            }

            _context.Entry(catalogo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent(); // HTTP 204 (sin contenido)
        }

        // DELETE /api/catalogo/{id}: Eliminar un catálogo específico
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogo(int id)
        {
            var catalogo = await _context.Catalogos.FindAsync(id);
            if (catalogo == null)
            {
                return NotFound("Catálogo no encontrado.");
            }

            _context.Catalogos.Remove(catalogo);
            await _context.SaveChangesAsync();

            return NoContent(); // HTTP 204 (sin contenido)
        }
    }
}
