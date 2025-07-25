using System.ComponentModel.DataAnnotations;
namespace LaCazuelaChapinaAPI.Models
{
    public class Inventario
    {
        [Key]
        public int IdInventario { get; set; }
        public int IdProductoFk { get; set; }
        public int IdTipoFk { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaIngreso { get; set; }

        // Relación con los productos (tamales)
        public virtual Tamal Producto { get; set; }
        public virtual CatalogoItem Tipo { get; set; }
    }
}
