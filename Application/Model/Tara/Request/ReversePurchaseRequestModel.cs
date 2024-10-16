using Newtonsoft.Json;
namespace Application.Model.Tara.Request;

public sealed record ReversePurchaseRequestModel
{
    [JsonIgnore]
    public string? CashDeskToken { get; set; }
    public string? traceNumber { set; get; }
}
