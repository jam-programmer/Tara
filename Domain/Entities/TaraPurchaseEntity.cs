using Domain.Enum;

namespace Domain.Entities;

public class TaraPurchaseEntity:BaseEntity
{
    public string? code { get; set; }    
    public string? message { get; set; }    
    public string? doTime { get; set; }    
    public string? traceNumber { get; set; }    
    public string? referenceNumber { get; set; }  
    public string? terminal { get; set; }

    public string? PaymentReferenceNumber { set; get; }
    public string? Type { set; get; }

    public PurchaseEnum PurchaseType { set; get; }
    public Guid OrderId { set; get; }
    public OrderEntity? Order { set; get; }
    public DateTime? CreatedDate { set; get; }=DateTime.Now;
}
