
using Mapster;

namespace Application.Services.Purchase;

public class Purchase : IPurchase
{
    private readonly ITaraWebService _taraWebService;


    public Purchase(ITaraWebService taraWebService)
    {
        _taraWebService = taraWebService;
    }

    public async Task<List<ProductGroupViewModel>>? 
        GetProductGroupsAsync(CancellationToken cancellation = default)
    {
        List<ProductGroupResponseModel>? groups =
            await _taraWebService.GetProductGroupAsync(cancellation)!;
        if (groups is null)
        {
            return null;
        }
        List<ProductGroupViewModel>
            productGroups = groups.Adapt<List<ProductGroupViewModel>>();
        return productGroups;

    }
}
