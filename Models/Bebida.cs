using System.ComponentModel.DataAnnotations;
namespace LaCazuelaChapinaAPI.Models
{
    public class Bebida
    {
        [Key]
        public int IdBebida { get; set; }

        // Claves foráneas para las relaciones
        public int IdTipoBebida { get; set; }
        public int IdTamanoFk { get; set; }
        public int IdEndulzanteFk { get; set; }
        public int IdToppingFk { get; set; }

        // Precio de la bebida
        public decimal Precio { get; set; }

        // Inventario disponible
        public int Inventario { get; set; }

        // Relación con otras tablas (para las claves foráneas)
        public virtual CatalogoItem TipoBebida { get; set; }
        public virtual CatalogoItem Tamano { get; set; }
        public virtual CatalogoItem Endulzante { get; set; }
        public virtual CatalogoItem Topping { get; set; }
    }
}
