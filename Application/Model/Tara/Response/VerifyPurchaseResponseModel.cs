namespace Application.Model.Tara.Response;

public record VerifyPurchaseResponseModel
{
    public bool success { get; set; }
    public int code { get; set; }
    public string? message { get; set; }
    public string? doTime { get; set; }
    public string? traceNumber { get; set; }
    public string? referenceNumber { get; set; }
}
