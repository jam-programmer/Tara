namespace Application.Services.Pos;

public interface IPos
{
    Task<List<PosViewModel>> GetPosesAsync(CancellationToken cancellation = default);
    Task<Result> GetMerchandiseGroupsAsync
        (string terminal, CancellationToken cancellation = default);
    Task<Result<Guid>> RegisterOrderAsync(RegisterOrderDto registerOrder,
        CancellationToken cancellation = default);

    Task<Result<Guid>> PurchaseRequestTaraAsync(Guid orderId,
        string terminal
        , CancellationToken cancellation = default);


    Task<Result> VerifyPurchaseTaraAsync(Guid orderId, string terminal, CancellationToken cancellation = default);
    Task<Result> ReversePurchaseTaraAsync(Guid orderId, string terminal, CancellationToken cancellation = default);

}
