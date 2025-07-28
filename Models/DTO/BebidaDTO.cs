namespace LaCazuelaChapinaAPI.Models.DTO
{
  public class BebidaDto
  {
    public int IdTipoBebida { get; set; }
    public int IdTamanioFk { get; set; }
    public int IdEndulzanteFk { get; set; }
    public int IdToppingFk { get; set; }
    public decimal Precio { get; set; }
    public int Inventario { get; set; }
  }
}
