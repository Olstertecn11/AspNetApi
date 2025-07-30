using System.ComponentModel.DataAnnotations;
namespace LaCazuelaChapinaAPI.Models
{
  public class DetalleVenta
  {
    [Key]
    public int IdDetalleVenta { get; set; }
    public int IdVentaFk { get; set; }  // Se completa en el backend
    public int IdProductoFk { get; set; }
    public int IdTipoProducto { get; set; } // 1 = Tamal, 2 = Bebida
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
  }
}
