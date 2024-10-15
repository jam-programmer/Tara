using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Order;

public record OrderDto
{
    public string? amount { set; get; }
    
    public string? mobile { set; get; }
   
    //public string? orderId { set; get; }
    
    public long vat { set; get; }
    public List<OrderItemDto>? products { set; get; }
}
