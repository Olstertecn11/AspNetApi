using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaCazuelaChapinaAPI.Models
{
    [Table("bebida", Schema = "cazuela_chapina")]
    public class Bebida
    {
        [Key]
        [Column("id_bebida")]
        public int IdBebida { get; set; }

        [Column("id_tipo_bebida")]
        public int IdTipoBebida { get; set; }

        [Column("id_tamanio_fk")]
        public int IdTamanioFk { get; set; }

        [Column("id_endulzante_fk")]
        public int IdEndulzanteFk { get; set; }

        [Column("id_topping_fk")]
        public int IdToppingFk { get; set; }

        [Column("precio")]
        public decimal Precio { get; set; }

        [Column("inventario")]
        public int Inventario { get; set; }

        public virtual CatalogoItem TipoBebida { get; set; }
        public virtual CatalogoItem Tamanio { get; set; }
        public virtual CatalogoItem Endulzante { get; set; }
        public virtual CatalogoItem Topping { get; set; }
    }
}
