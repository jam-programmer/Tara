using System.ComponentModel.DataAnnotations;

namespace TaraService.Models.WebService;

public record PurchaseRequestModel
{
    [Required]
    public string? amount { set; get; }
    [Required]
    public string? mobile { set; get; }
    [Required]
    public string? orderId { set; get; }
    [Required]
    public long vat { set; get; }
    [Required]
    public List<PurchaseProduct>? products { set; get; }
}
