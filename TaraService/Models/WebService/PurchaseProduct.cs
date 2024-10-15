using Application.Common;
using System.ComponentModel.DataAnnotations;

namespace TaraService.Models.WebService;

public record PurchaseProduct
{
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "nameRequired", ErrorMessageResourceType = typeof(Message))]
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "codeRequired", ErrorMessageResourceType = typeof(Message))]
    public string code { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "countRequired", ErrorMessageResourceType = typeof(Message))]
    public long count { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "unitRequired", ErrorMessageResourceType = typeof(Message))]
    public long unit { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "feeRequired", ErrorMessageResourceType = typeof(Message))]
    public long fee { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "groupRequired", ErrorMessageResourceType = typeof(Message))]
    public string group { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "groupTitleRequired", ErrorMessageResourceType = typeof(Message))]
    public string groupTitle { get; set; }
    /// <summary>
    /// 
    /// </summary>
  
    public string data { get; set; }
}
