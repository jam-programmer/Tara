using Application.Common;
using Application.Contracts;
using Application.Model.Tara.Request;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;


namespace Application.Services.Tara;

public class TaraWebService : ITaraWebService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<TaraWebService> _logger;
    private readonly IApiService _apiService;

    public TaraWebService(IApiService apiService, ILogger<TaraWebService> logger, IDistributedCache cache)
    {
        _cache = cache;
        _logger = logger;
        _apiService = apiService;
    }

    #region Purchase In Person


    public async Task<PurchaseResponseModel>? PurchaseAsync(PurchaseRequestModel request)
    {
        PurchaseResponseModel purchase = await _apiService.PostAsync<PurchaseResponseModel>(new ApiOption()
        {
            BaseUrl = DefaultData.BaseUrl + "api/v1/purchase/request/",
            BearerToken = request.CashDeskToken,
            DataBody=request
        });
        return purchase;
    }
    public async Task<List<MerchandiseGroupResponseModel>>? GetMerchandiseGroupAsync(MerchandiseGroupRequestModel request)
    {
        List<MerchandiseGroupResponseModel> merchandise = await _apiService.PostAsync<List<MerchandiseGroupResponseModel>>(new ApiOption()
        {
            BaseUrl = DefaultData.BaseUrl + "api/v1/purchase/merchandise/groups",
            BearerToken = request.paymentRegisterPurchaseToken
        });
        return merchandise;
    }

    public async Task<List<MerchantAccessResponseModel>>? GetMerchantAccessesAsync()
    {
        TemporaryTokenResponseModel? temporaryToken=await GetTemporaryTokenFromCacheAsync();
        List<MerchantAccessResponseModel> accesses = await _apiService.PostAsync<List<MerchantAccessResponseModel>>(new ApiOption()
        {
            BaseUrl = DefaultData.BaseUrl + "api/v1/merchant/access/code/",
            BearerToken = temporaryToken!.accessCode,
         
            DataBody = new
            {
                DefaultData.branchCode
            }
        });
        return accesses;
    }
    private async Task<TemporaryTokenResponseModel>? GetTemporaryTokenFromCacheAsync()
    {
        TemporaryTokenResponseModel? temporaryToken;
        var cacheValue = await _cache.GetAsync(DefaultData.TemporaryTokenCacheKry);
        if (cacheValue == null)
        {
            temporaryToken = await GetTemporaryTokenAsync();
            TimeSpan timeSpan = TimeSpan.FromSeconds(temporaryToken.expiryDuration);
            string serializedSetting = JsonSerializer.Serialize(temporaryToken);
            byte[] settingEncoded = Encoding.UTF8.GetBytes(serializedSetting);
            await _cache.SetAsync(DefaultData.TemporaryTokenCacheKry, settingEncoded,
              new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(timeSpan));
        }
        else
        {
            temporaryToken = JsonSerializer.Deserialize<TemporaryTokenResponseModel>(cacheValue);
        }
        return temporaryToken;
    }

    private async Task<TemporaryTokenResponseModel>?
        GetTemporaryTokenAsync(CancellationToken cancellation = default)
    {
        TemporaryTokenResponseModel response = await _apiService.PostAsync<TemporaryTokenResponseModel>(new ApiOption()
        {
            BaseUrl = DefaultData.BaseUrl + "api/v1/user/login/merchant",
            DataBody = new
            {
                DefaultData.principal,
                DefaultData.password
            }
        });
        return response;
    }


    public async Task<TrackingCodeResponseModel>? GetTrackingCodeAsync(PaymentInformationRequestModel request)
    {
        TemporaryTokenResponseModel? temporaryToken = await GetTemporaryTokenFromCacheAsync();
        TrackingCodeResponseModel accesses = await _apiService.PostAsync<TrackingCodeResponseModel>(new ApiOption()
        {
            BaseUrl = DefaultData.BaseUrl + "/api/v1/purchase/trace",
            BearerToken = request.AccessToken,
            DataBody = request

        });
        return accesses;
    }

    public async Task<VerifyPurchaseResponseModel>? VerifyPurchaseAsync(VerifyPurchaseRequestModel request)
    {
        VerifyPurchaseResponseModel verifyPurchase =
            await _apiService.PostAsync<VerifyPurchaseResponseModel>(new ApiOption()
        {
            BaseUrl = DefaultData.BaseUrl + "api/v1/purchase/verify",
            BearerToken = request.CashDeskToken,
            DataBody = request
        });
        return verifyPurchase;
    }

    public async Task<ReversePurchaseResponseModel>? ReversePurchaseAsync(ReversePurchaseRequestModel request)
    {
        ReversePurchaseResponseModel reversePurchase =
           await _apiService.PostAsync<ReversePurchaseResponseModel>(new ApiOption()
           {
               BaseUrl = DefaultData.BaseUrl + "api/v1/purchase/reverse",
               BearerToken = request.CashDeskToken,
               DataBody = request
           });
        return reversePurchase;
    }



    #endregion





    public async Task<AuthenticateResponseModel>? AuthenticateAsync()
    {
        AuthenticateResponseModel response = 
            await _apiService.PostAsync<AuthenticateResponseModel>(new ApiOption()
        {
            BaseUrl = DefaultData.BaseUrlOnlinePurchase + "api/v2/authenticate",
            DataBody = new
            {
                DefaultData.OnlinePurchaseUserName,
                DefaultData.OnlinePurchasePassword
            }
        });
        return response;
    }
    private async Task<AuthenticateResponseModel>? GetTemporaryAuthenticateTokenFromCacheAsync()
    {
        AuthenticateResponseModel? temporaryAuthenticateToken;
        var cacheValue = await _cache.GetAsync(DefaultData.TemporaryAuthenticateTokenCacheKry);
        if (cacheValue == null)
        {
            temporaryAuthenticateToken = await AuthenticateAsync();

            TimeSpan timeSpan = TimeSpan.FromSeconds(temporaryAuthenticateToken.expireTime!.Value);
            string serializedSetting = JsonSerializer.Serialize(temporaryAuthenticateToken);

            byte[] settingEncoded = Encoding.UTF8.GetBytes(serializedSetting);
            await _cache.SetAsync(DefaultData.TemporaryAuthenticateTokenCacheKry, settingEncoded,
              new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(timeSpan));
        }
        else
        {
            temporaryAuthenticateToken = JsonSerializer.Deserialize<AuthenticateResponseModel>(cacheValue);
        }
        return temporaryAuthenticateToken;
    }

    public async Task<List<ProductGroupResponseModel>>? GetProductGroupAsync(CancellationToken cancellationToken = default)
    {
        AuthenticateResponseModel? authenticate = await
            GetTemporaryAuthenticateTokenFromCacheAsync()!;
        if (authenticate is null)
        {
            //Todo Log
        }
        List<ProductGroupResponseModel> groups =
            await _apiService.PostAsync<List<ProductGroupResponseModel>>
            (new ApiOption()
            {
                BaseUrl = DefaultData.BaseUrlOnlinePurchase
                + "api/clubGroups",
               BearerToken=authenticate!.accessToken
            });
        return groups;
    }
}
