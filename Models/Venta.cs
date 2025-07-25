using System.ComponentModel.DataAnnotations;
namespace LaCazuelaChapinaAPI.Models
{
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }
        public int IdUsuarioFk { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Monto { get; set; }

        // Relación con el usuario
        public virtual Usuario Usuario { get; set; }

        // Relación con los detalles de la venta
        public virtual List<DetalleVenta> DetalleVentas { get; set; }
    }
}
