namespace TaraService.Models.WebService;

public record BackPaymentGatewayModel
{
    public string result {  get; set; } 
    public string desc {  get; set; } 
    public string token {  get; set; } 
    public string channelRefNumber {  get; set; } 
    public string additionalData {  get; set; } 
    public string orderId {  get; set; } 
}
