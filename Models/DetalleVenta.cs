using System.ComponentModel.DataAnnotations;
namespace LaCazuelaChapinaAPI.Models
{
    public class DetalleVenta
    {
        [Key]
        public int IdDetalleVenta { get; set; }
        public int IdVentaFk { get; set; }
        public int IdProductoFk { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }

        // Relación con la venta y el producto
        public virtual Venta Venta { get; set; }
        public virtual Tamal Producto { get; set; }
    }
}
