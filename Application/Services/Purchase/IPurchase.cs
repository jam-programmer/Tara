namespace Application.Services.Purchase;

public interface IPurchase
{
    Task<List<ProductGroupViewModel>>
        GetProductGroupsAsync(CancellationToken
        cancellation=default);
    Task SavePaymentGatewayResponseAsync(PaymentGatewayResponseModel? model=null
        ,CancellationToken cancellation=default);
    Task<Result> PurchaseRequestAsync();
}
