using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("usuario", Schema = "autenticacion")]
public class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("nombre")]
    public string? Nombre { get; set; }

    [Required]
    [Column("correo")]
    public string Correo { get; set; }

    [Required]
    [Column("contrasenia")]
    public string Contrasenia { get; set; }  // <-- Sin "ñ"

    [Column("rol_id_fk")]
    public int? RolIdFk { get; set; }

    [Column("fh_ingreso")]
    public DateTime? FhIngreso { get; set; }
}
