namespace Domain.Entities;

public class MerchantAccessEntity:BaseEntity
{
    public DateTime? CreatedDate { set; get; } = DateTime.Now;
    public string? accessCode { get; set; }
    public string? merchantCode { get; set; }
    public string? terminalCode { get; set; }
    public string? terminalTitle { get; set; }
}
