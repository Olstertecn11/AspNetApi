using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaCazuelaChapinaAPI.Models
{
    public class Tamal
    {
        [Key]
        [Column("id_tamal")]
        public int IdTamal { get; set; }

        [Column("id_tipo_masa_fk")]
        public int IdTipoMasaFk { get; set; }

        [Column("id_relleno_fk")]
        public int IdRellenoFk { get; set; }

        [Column("id_envoltura_fk")]
        public int IdEnvolturaFk { get; set; }

        [Column("id_nivel_picante")]
        public int IdNivelPicante { get; set; }

        [Column("precio")]
        public decimal Precio { get; set; }

        [Column("inventario")]
        public int Inventario { get; set; }

        // Relaciones
        public virtual CatalogoItem TipoMasa { get; set; }
        public virtual CatalogoItem Relleno { get; set; }
        public virtual CatalogoItem Envoltura { get; set; }
        public virtual CatalogoItem NivelPicante { get; set; }
    }
}
