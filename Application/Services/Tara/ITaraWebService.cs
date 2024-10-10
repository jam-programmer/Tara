
using Application.Model.Tara.Request;

namespace Application.Services.Tara;

public interface ITaraWebService
{

    Task<List<MerchantAccessResponseModel>>? 
        GetMerchantAccessesAsync();


 
    Task<TrackingCodeResponseModel> ?
        GetTrackingCodeAsync(PaymentInformationRequestModel request);


  
    Task<PurchaseResponseModel>?
        PurchaseAsync(PurchaseRequestModel request);



   
    Task<List<MerchandiseGroupResponseModel>>?
        GetMerchandiseGroupAsync(MerchandiseGroupRequestModel request);

    Task<VerifyPurchaseResponseModel>
        ? VerifyPurchaseAsync(VerifyPurchaseRequestModel request); 
    
    Task<ReversePurchaseResponseModel>
        ? ReversePurchaseAsync(ReversePurchaseRequestModel request);

}
