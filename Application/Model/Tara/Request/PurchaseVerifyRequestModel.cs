namespace Application.Model.Tara.Request;

public sealed record PurchaseVerifyRequestModel
{
    public string token { set; get; }
    public string result { set; get; }
    public string description { set; get; }
    public string doTime { set; get; }
    public string amount { set; get; }
    public string rrn { set; get; }
    public string type { set; get; }

}
