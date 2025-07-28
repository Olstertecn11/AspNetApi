using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LaCazuelaChapinaAPI.Data;
using LaCazuelaChapinaAPI.Models;
using LaCazuelaChapinaAPI.Models.DTO;

namespace LaCazuelaChapinaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Correo == request.Correo))
            {
                return Conflict(new { message = "Ya existe un usuario con este correo." });
            }

            var user = new Usuario
            {
                Nombre = request.Nombre,
                Correo = request.Correo,
                RolIdFk = request.RolIdFk ?? 0,
                FhIngreso = DateTime.UtcNow
            };

            // Hashear contrasenia
            user.Contrasenia = _passwordHasher.HashPassword(user, request.Contrasenia);

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario registrado exitosamente." });
        }
    }
}
