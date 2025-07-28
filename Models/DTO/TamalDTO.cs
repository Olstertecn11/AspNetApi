namespace LaCazuelaChapinaAPI.Models.DTO
{
    public class TamalDto
    {
        public int IdTipoMasaFk { get; set; }
        public int IdRellenoFk { get; set; }
        public int IdEnvolturaFk { get; set; }
        public int IdNivelPicante { get; set; }
        public decimal Precio { get; set; }
        public int Inventario { get; set; }
    }
}
