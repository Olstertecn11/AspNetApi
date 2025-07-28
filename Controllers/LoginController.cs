using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LaCazuelaChapinaAPI.Data;
using LaCazuelaChapinaAPI.Models;
using LaCazuelaChapinaAPI.Models.DTO;

namespace LaCazuelaChapinaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public LoginController(ApplicationDbContext context, IConfiguration config)
        {
          _context = context;
          _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == request.Correo);

            if (user == null)
                return Unauthorized(new { message = "Usuario no encontrado" });

            var hasher = new PasswordHasher<Usuario>();
            var result = hasher.VerifyHashedPassword(user, user.Contrasenia, request.Contrasenia);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(new { message = "Contraseña incorrecta" });

            // Crear claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, user.Nombre ?? ""),
                new Claim(ClaimTypes.Email, user.Correo),
                new Claim(ClaimTypes.Role, user.RolIdFk.ToString())
            };

            // Obtener clave desde configuración
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear token JWT
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            // Retornar token y datos del usuario
            return Ok(new
            {
                message = "Inicio de sesión exitoso",
                token = new JwtSecurityTokenHandler().WriteToken(token),
                usuario = new
                {
                    user.IdUsuario,
                    user.Nombre,
                    user.Correo,
                    user.RolIdFk
                }
            });
        }
    }
}
