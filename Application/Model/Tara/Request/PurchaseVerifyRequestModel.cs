using System.Text.Json.Serialization;

namespace Application.Model.Tara.Request;

public sealed record PurchaseVerifyRequestModel
{
    [JsonIgnore]
    public string? accessToken { set; get; }
    public string? ip { set; get; }   
}
