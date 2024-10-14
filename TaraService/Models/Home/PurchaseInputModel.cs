namespace Tara.Models.Home;

public record PurchaseInputModel
{
   public string? terminal {set;get;}
   public Guid orderId { set; get; }
}
