namespace Domain.Entities;

public class OrderDetailEntity:BaseEntity
{
    public DateTime? CreatedDate { set; get; } = DateTime.Now;
    public string? ProductName { set; get; }
    public string? ProductCode { set; get; }
    public float Count { set; get; }
    public int Unit {  set; get; }  
    public long Fee { set; get; }
    public string? Group { set; get; }
    public string? GroupTitle { set; get; }
    public int Made { set; get; }
    public Guid OrderId { set; get; }
    public OrderEntity order { set; get; }  

}
