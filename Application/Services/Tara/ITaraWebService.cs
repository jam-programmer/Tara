namespace Application.Services.Tara;

public interface ITaraWebService
{
    #region Purchase In Person

    Task<List<MerchantAccessResponseModel>>?
        GetMerchantAccessesAsync();
    Task<TrackingCodeResponseModel>?
        GetTrackingCodeAsync(PaymentInformationRequestModel request);
    Task<PurchaseResponseModel>?
        PurchaseAsync(PurchaseRequestModel request);
    Task<List<MerchandiseGroupResponseModel>>?
        GetMerchandiseGroupAsync(MerchandiseGroupRequestModel request);
    Task<VerifyPurchaseResponseModel>
        ? VerifyPurchaseAsync(VerifyPurchaseRequestModel request);
    Task<ReversePurchaseResponseModel>
        ? ReversePurchaseAsync(ReversePurchaseRequestModel request);
    #endregion



    #region Internet Shopping

    Task<List<ProductGroupResponseModel>>?
        GetProductGroupAsync(CancellationToken cancellationToken=default);

    Task<TokenPaymentGatewayResponseModel>? GetTokenPaymentGatewayAsync(
        TokenPaymentGatewayRequestModel request 
        ,CancellationToken cancellationToken=default);


    Task GoToIpgPurchaseAsync(IpgPurchaseRequestModel request);
    #endregion

}
