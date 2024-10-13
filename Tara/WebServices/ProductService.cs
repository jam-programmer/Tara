using Carter;

namespace Tara.WebServices;

public class ProductService : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder baseRoute = app.MapGroup("/Product");
    }
}
