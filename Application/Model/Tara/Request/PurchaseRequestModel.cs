using Newtonsoft.Json;
namespace Application.Model.Tara.Request;

public record PurchaseRequestModel
{
    [JsonIgnore]
    public string? CashDeskToken { get; set; }
    public string? traceNumber { set; get; }
    public long amount {  set; get; }   
    public string? invoiceNumber { set; get; }
    public string? data { set; get; }
    public InvoiceData? invoiceData { set; get; }
}
