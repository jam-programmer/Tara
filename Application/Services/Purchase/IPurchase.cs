using Application.Dto.Purchase;

namespace Application.Services.Purchase;

public interface IPurchase
{
    Task<List<ProductGroupViewModel>>
        GetProductGroupsAsync(CancellationToken
        cancellation=default);
    
    Task PurchaseRequestAsync(OrderDto order,CancellationToken cancellation=default);

    Task PurchaseVerifyAsync(VerifyDto verify,CancellationToken cancellation=default);  



}
