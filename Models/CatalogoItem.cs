using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LaCazuelaChapinaAPI.Models
{
    public class CatalogoItem
    {
        [Key]
        [Column("id_catalogo_item")]
        public int IdCatalogoItem { get; set; }

        [Column("id_catalogo")]
        public int IdCatalogo { get; set; }

        [Column("item_nombre")]
        public string ItemNombre { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }


        [Column("esta_activo")]
        public bool EstaActivo { get; set; }



        [JsonIgnore]
        public virtual Catalogo? Catalogo { get; set; }
    }
}
