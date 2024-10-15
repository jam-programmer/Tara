using Application.Common;
using System.ComponentModel.DataAnnotations;

namespace TaraService.Models.WebService;

public record PurchaseRequestModel
{
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "amountRequired", ErrorMessageResourceType =typeof(Message))]
    public string? amount { set; get; }
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "mobileRequired", ErrorMessageResourceType = typeof(Message))]
    public string? mobile { set; get; }
    /// <summary>
    /// 
    /// </summary>
    //[Required(ErrorMessage = "orderIdRequired", ErrorMessageResourceType = typeof(Message))]
    //public string? orderId { set; get; }
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "vatRequired", ErrorMessageResourceType = typeof(Message))]
    public long vat { set; get; }
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "productsRequired", ErrorMessageResourceType = typeof(Message))]
    public List<PurchaseProduct>? products { set; get; }
}
