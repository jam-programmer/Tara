namespace Application.Model.Tara.Request;

public record InvoiceDataItem
{
    public string? name { set; get; }
    public string? code { set; get; }
    public float count { set; get; }
    public int unit { set; get; }
    public long fee { set; get; }
    public string? group {  set; get; }
    public string? groupTitle {  set; get; }
    public int made { set; get; }
    public string? data { set; get; }
}
