using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LaCazuelaChapinaAPI.Models
{
    public class Catalogo
    {
        [Key]
        public int IdCatalogo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EstaActivo { get; set; } = true;

        [JsonIgnore]
        public virtual ICollection<CatalogoItem>? CatalogoItems { get; set; }
    }
}
