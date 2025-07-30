namespace LaCazuelaChapinaAPI.Models.DTO{

  public class VentaRequestDto
  {
    public int IdUsuarioFk { get; set; }
    public DateTime FhIngreso { get; set; }
    public decimal Total { get; set; }

    public ItemsDto Items { get; set; }
  }

  public class ItemsDto
  {
    public List<TamalItemDto> Tamales { get; set; }
    public List<BebidaItemDto> Bebidas { get; set; }
  }

  public class TamalItemDto
  {
    public int IdTamal { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
  }

  public class BebidaItemDto
  {
    public int IdBebida { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
  }
}
