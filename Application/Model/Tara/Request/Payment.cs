namespace Application.Model.Tara.Request;

public sealed record Payment
{
    public string? barcode { set; get; } 
    public long amount { set; get; } = 157400;
    public string? data {  set; get; }  
}
