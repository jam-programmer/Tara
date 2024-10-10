namespace Application.Model.Tara.Request;

public record InvoiceData
{
    public long totalPrice { set; get; }
    public string? invoiceNumber {  set; get; }
    public string? data { set; get; }
    public long vat { set; get; }
    public List<InvoiceDataItem>? items { set; get; }

}
