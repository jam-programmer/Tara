using Domain.Enum;

namespace Domain.Entities;

public class OrderEntity:BaseEntity
{
    /// <summary>
    /// 
    /// </summary>
    public PurchaseTypeEnum Type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime? CreatedDate { set; get; } = DateTime.Now;
    /// <summary>
    /// 
    /// </summary>
    public string? WalletBarcode { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? OrderDate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long Amount {  get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? InvoiceNumber { set; get; }
    /// <summary>
    /// 
    /// </summary>
    public long ValueAddedTax { get; set; } = 1000;
    /// <summary>
    /// 
    /// </summary>
    public string? CustomerFullName {  get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? CustomerPhoneNumber {  get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long TotalPrice {  get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<OrderDetailEntity>? Details { set; get; }
    /// <summary>
    /// 
    /// </summary>
    public OrderTrackingEntity? OrderTracking { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<TaraPurchaseEntity>? TaraPurchases { get; set; }

}
