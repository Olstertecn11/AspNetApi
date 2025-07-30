using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("venta", Schema = "cazuela_chapina")]
public class Venta
{
    [Key]
    public int IdVenta { get; set; }
    public int IdUsuarioFk { get; set; }
    public DateTime FhIngreso { get; set; }
    public decimal Monto { get; set; }
}
