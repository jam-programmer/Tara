namespace Application.Model.Tara.Request;

public sealed record PaymentInformationRequestModel
{
    public string? AccessToken { set; get; }
    public string? terminalCode {  get; set; } 
    public List<Payment>? payment { get; set; }
}
