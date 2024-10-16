using Application.Common;
using Application.Dto.Order;
using Application.Model.Tara.Response;
using Application.Services.Purchase;
using Carter;
using Mapster;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using TaraService.Models.WebService;
using Microsoft.AspNetCore.Http.Headers;

namespace Tara.WebServices;

public class PaymentService : ICarterModule
{

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder baseRoute = app.MapGroup("/TaraService");

        baseRoute.MapPost("/BackPaymentGateway", async ([FromBody] BackPaymentGatewayModel response,
            IPurchase purchase) =>
        {


        });



        baseRoute.MapPost("/PurchaseRequest", async ([FromBody] PurchaseRequestModel request,
            IPurchase purchase) =>
        {
            if (request == null)
            {
                return Results.BadRequest(ResponseBase.Fail(Message.InputUnclear));
            }
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(request);
            bool isValid = Validator.TryValidateObject(request, validationContext, validationResults, true);
            if (!isValid)
            {
                List<string>? errorMessages = validationResults.Select(vr => vr.ErrorMessage).ToList()!;
                return Results.BadRequest(ResponseBase.Fail(Message.RequestNotValid,errorMessages));
            }
            List<ValidationResult> validationProductsResults = new List<ValidationResult>();
            ValidationContext validationProductsContext = new ValidationContext(request.products);
            bool isValidroducts = Validator.TryValidateObject(request.products, validationProductsContext, validationProductsResults, true);
            if (!isValidroducts)
            {
                List<string>? errorMessages = validationProductsResults.Select(vr => vr.ErrorMessage).ToList()!;
                return Results.BadRequest(ResponseBase.Fail(Message.RequestNotValid, errorMessages));
            }

            List<OrderItemDto> orderItems = new();
            orderItems = request.products.Adapt<List<OrderItemDto>>();
            OrderDto order = new();
            order=request.Adapt<OrderDto>();
            order.products = orderItems;
            await purchase.PurchaseRequestAsync(order);
            return Results.Ok();
        });


    }
}
