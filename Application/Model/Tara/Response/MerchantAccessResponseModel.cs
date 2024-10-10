
namespace Application.Model.Tara.Response
{
    public record MerchantAccessResponseModel
    {
        public string? accessCode { get; set; }
        public string? merchantCode { get; set; }
        public string? terminalCode { get; set; }
        public string? terminalTitle { get; set; }
        public string? identifier { get; set; }
        public object? accessibleType { get; set; }
    }
}
