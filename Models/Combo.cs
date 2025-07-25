using System.ComponentModel.DataAnnotations;
namespace LaCazuelaChapinaAPI.Models
{
    public class Combo
    {
        [Key]
        public int IdCombo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Items { get; set; } // Aquí podemos almacenar un JSON con los productos del combo
    }
}
