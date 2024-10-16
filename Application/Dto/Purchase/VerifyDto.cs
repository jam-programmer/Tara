
namespace Application.Dto.Purchase;

public record VerifyDto
{
    public string token { get; set; }
    public string orderId { get; set; }
}
