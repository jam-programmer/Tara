namespace Application.Services.Purchase;

public interface IPurchase
{
    Task<List<ProductGroupViewModel>>
        GetProductGroupsAsync(CancellationToken
        cancellation=default);
}
