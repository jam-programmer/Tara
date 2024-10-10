namespace Domain.Entities;

public class OrderTrackingEntity:BaseEntity
{
    public DateTime? CreatedDate { set; get; } = DateTime.Now;
    public string? TraceNumber { set; get; }
    public string? Mobile { set; get; }
    public string? DoTime { set; get; }
    public string? Message { set; get; }
    public long Code { set; get; }
    public Guid OrderId { set; get; }
    public OrderEntity? Order { set; get; }  
}
