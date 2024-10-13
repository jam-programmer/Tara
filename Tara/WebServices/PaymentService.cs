using Application.Services.Purchase;
using Application.ViewModel.PurchaseViewModels;
using Carter;

namespace Tara.WebServices;

public class PaymentService : ICarterModule
{
  
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder baseRoute = app.MapGroup("/TaraService");
        baseRoute.MapGet("/GetProductGroup", async (IPurchase _purchase) =>
        {

            List<ProductGroupViewModel> result =
            await _purchase.GetProductGroupsAsync();
            if (result is null)
            {
                return Results.StatusCode(500);
            }
            if (result.Count() == 0)
            {
                return Results.StatusCode(204);
            }
            return Results.Ok();
        });
    }
}
