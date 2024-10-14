using Application.Common;
using Application.Model.Tara.Response;
using Application.Services.Purchase;
using Carter;
using Mapster;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using TaraService.Models.WebService;

namespace Tara.WebServices;

public class PaymentService : ICarterModule
{

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder baseRoute = app.MapGroup("/TaraService");

        baseRoute.MapPost("/BackPaymentGateway", async ([FromBody] BackPaymentGatewayModel response,
            IPurchase purchase) =>
        {
            await purchase.SavePaymentGatewayResponseAsync(response.Adapt<PaymentGatewayResponseModel>());
        });

        
        baseRoute.MapPost("/PurchaseRequest", async ([FromBody] PurchaseRequestModel request, IPurchase purchase) =>
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
        });


    }
}
