namespace Domain.Entities;

public class OrderEntity:BaseEntity
{
    public DateTime? CreatedDate { set; get; } = DateTime.Now;
    public string? WalletBarcode { get; set; }
    public string? OrderDate { get; set; }
    public long Amount {  get; set; }
    public string? InvoiceNumber { set; get; }
    public long ValueAddedTax { get; set; } = 1000;
    public string? CustomerFullName {  get; set; }
    public string? CustomerPhoneNumber {  get; set; }
    public long TotalPrice {  get; set; }
    public List<OrderDetailEntity>? Details { set; get; }
    public OrderTrackingEntity? OrderTracking { get; set; }
    public List<TaraPurchaseEntity>? TaraPurchases { get; set; }
}
