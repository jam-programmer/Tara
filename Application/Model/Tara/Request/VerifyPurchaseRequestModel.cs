using Newtonsoft.Json;
namespace Application.Model.Tara.Request;

public record VerifyPurchaseRequestModel
{
    [JsonIgnore]
    public string? CashDeskToken { get; set; }
    public string? traceNumber { set; get; }
}
