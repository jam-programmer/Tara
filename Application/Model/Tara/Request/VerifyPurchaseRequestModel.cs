using Newtonsoft.Json;
namespace Application.Model.Tara.Request;

public sealed record VerifyPurchaseRequestModel
{
    [JsonIgnore]
    public string? CashDeskToken { get; set; }
    public string? traceNumber { set; get; }
}
