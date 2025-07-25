using System.ComponentModel.DataAnnotations;
namespace LaCazuelaChapinaAPI.Models
{
    public class Catalogo
    {
        [Key]
        public int IdCatalogo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // Relación con los items del catálogo
        public virtual ICollection<CatalogoItem> CatalogoItems { get; set; }
    }
}
