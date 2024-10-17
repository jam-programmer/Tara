using Application.Services.Purchase;
using Carter;
using Application.ViewModel.PurchaseViewModels;
using TaraService.Models.WebService;


namespace Tara.WebServices;

public class ProductService : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder baseRoute = app.MapGroup("/api/v1/Product");
        baseRoute.MapGet("/GetProductGroup", async (IPurchase _purchase) =>
        {
            try
            {
                List<ProductGroupViewModel> result =
                        await _purchase.GetProductGroupsAsync();
                if (result is null)
                {
                    return Results.StatusCode(500);
                }
                if (result.Count() == 0)
                {
                    return Results.NoContent();
                }
                return Results.Ok(ResponseBase.Success(data: result));
            }
            catch (Exception ex)
            {
      
                return Results.StatusCode(500);
            }
        });
    }
}
