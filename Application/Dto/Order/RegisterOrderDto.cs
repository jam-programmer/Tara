
namespace Application.Dto.Order;

public record RegisterOrderDto
{
    public string? OrderDate { get; init; }
    public string? CustomerFullName { get; init; }
    public string? CustomerPhoneNumber { get; init; }
    public string? OrderDetail { get; init; }
    private string? _walletBarcode;

    public string? WalletBarcode
    {
        get => _walletBarcode;
        set => _walletBarcode = value?.Replace(" ", string.Empty);
    }

    public string? TerminalCode { get; init; }
}
