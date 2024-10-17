using Application.Model.Tara.Common;

namespace Application.Model.Tara.Request;

public sealed record TokenPaymentGatewayRequestModel
{
    public string? ip { set; get; }
    public string? additionalData { set; get; }
    public string? callBackUrl { set; get; } = "/TaraService/BackPaymentGateway";
    public string? amount { set; get; }
    public string? mobile { set; get; }
    public string? orderId { set; get; }
    public long? vat { set; get; }

    public List<ServiceAmount>? serviceAmountList { set; get; }    
    public List<TaraInvoiceItem>? taraInvoiceItemList { set; get; }    
}
