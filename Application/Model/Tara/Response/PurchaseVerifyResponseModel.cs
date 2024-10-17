using Application.Model.Tara.Common;

namespace Application.Model.Tara.Response;

public sealed record PurchaseVerifyResponseModel
{
    public string? token { set; get; }
    public string? result { set; get; }
    public string? description { set; get; }
    public string? doTime { set; get; }
    public string? amount { set; get; }
    public string? rrn { set; get; }
    public string? type { set; get; }
    public List<ServiceAmount>? serviceAmountList { set; get; }  
}
