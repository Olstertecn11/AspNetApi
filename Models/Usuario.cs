
using System.ComponentModel.DataAnnotations;

namespace LaCazuelaChapinaAPI.Models{
  public class Usuario{
    [Key]
    public int IdUsuario { get; set; }

    // Nombre del usuario
    public string Nombre { get; set; }

    // Correo del usuario (único)
    public string Correo { get; set; }

    // Contraseña del usuario
    public string Contraseña { get; set; }

    // Clave foránea que referencia al rol
    public int RolIdFk { get; set; }

    // Fecha de ingreso
    public DateTime FechaIngreso { get; set; }

    public int RolidFk {get;set;}


    // Propiedad de navegación para la relación con el rol
    public Rol Rol { get; set; }
  }
}
