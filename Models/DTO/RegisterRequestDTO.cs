namespace LaCazuelaChapinaAPI.Models.DTO
{
    public class RegisterRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;
        public int? RolIdFk { get; set; }
    }
}
