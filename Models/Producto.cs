namespace LaCazuelaChapinaAPI.Models
{
    public class Producto
    {
        public int IdProducto { get; set; } // Clave primaria

        // Nombre del producto (puede ser Tamal o Bebida)
        public string Nombre { get; set; }

        // Precio del producto
        public decimal Precio { get; set; }

        // Inventario del producto
        public int Inventario { get; set; }
    }
}
