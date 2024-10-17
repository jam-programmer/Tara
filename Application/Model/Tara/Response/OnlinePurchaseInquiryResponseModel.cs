namespace Application.Model.Tara.Response;

public sealed record OnlinePurchaseInquiryResponseModel
{
    public string result { get; set; }  
    public string description { get; set; }  
    public string doTime { get; set; }  
    public string orderId { get; set; }  
    public List<PurchaseVerifyResponseModel> trackPurchaseList { get; set; }   

}
