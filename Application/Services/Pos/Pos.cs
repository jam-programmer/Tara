
using Newtonsoft.Json;

namespace Application.Services.Pos;

public class Pos : IPos
{
    private readonly ITaraWebService _taraService;
    private readonly ILogger<Pos> _logger;
    private readonly IRepository<MerchantAccessEntity> _merchantRepository;
    private readonly IRepository<OrderEntity> _orderRepository;
    private readonly IRepository<OrderDetailEntity> _orderDetailRepository;
    private readonly IRepository<OrderTrackingEntity> _orderTrackingRepository;
    private readonly IRepository<TaraPurchaseEntity> _taraPurchaseRepository;

    public Pos(ITaraWebService taraService,
        ILogger<Pos> logger,
        IRepository<MerchantAccessEntity> merchantRepository,
        IRepository<OrderEntity> orderRepository,
        IRepository<OrderDetailEntity> orderDetailRepository,
        IRepository<OrderTrackingEntity> orderTrackingRepository,
        IRepository<TaraPurchaseEntity> taraPurchaseRepository
        )
    {
        _taraPurchaseRepository = taraPurchaseRepository;
        _orderTrackingRepository = orderTrackingRepository;
        _merchantRepository = merchantRepository;
        _logger = logger;
        _taraService = taraService;
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Result> GetMerchandiseGroupsAsync(string terminal, CancellationToken cancellation = default)
    {
        MerchantAccessEntity? entity = await _merchantRepository.
            GetAsync(e => e.terminalCode == terminal, cancellation);
        if (entity is null)
        {
            return Result.Fail(FailType.NotValidTerminal.GetEnumDescription());
        }
        MerchandiseGroupRequestModel request = new()
        {
            paymentRegisterPurchaseToken = entity.accessCode,
        };
        List<MerchandiseGroupResponseModel> groups = await _taraService.GetMerchandiseGroupAsync(request);
        if (groups.Any())
        {
            List<MerchandiseGroupViewModel> model = groups.Adapt<List<MerchandiseGroupViewModel>>();
            return Result<List<MerchandiseGroupViewModel>>.Success(data: model);
        }

        return Result<List<MerchandiseGroupViewModel>>.Fail(Message: FailType.NoContent.GetEnumDescription());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task<List<PosViewModel>> GetPosesAsync(CancellationToken cancellation = default)
    {
        List<MerchantAccessResponseModel> merchantAccesses
             = await _taraService.GetMerchantAccessesAsync();
        if (merchantAccesses.Any())
        {
            foreach (MerchantAccessResponseModel item in merchantAccesses)
            {
                bool existMerchantAccess = await _merchantRepository.
                    ExistAsync(
                            e => e.terminalTitle == item.terminalTitle &&
                               e.terminalCode == item.terminalCode &&
                               e.merchantCode == item.merchantCode, cancellation
                            );

                if (existMerchantAccess)
                {
                    MerchantAccessEntity? merchantAccess = await _merchantRepository.GetAsync(e => e.terminalTitle == item.terminalTitle &&
                               e.terminalCode == item.terminalCode &&
                               e.merchantCode == item.merchantCode);
                    merchantAccess!.accessCode = item.accessCode;
                    await _merchantRepository.UpdateAsync(merchantAccess, cancellation);

                }
                else
                {
                    MerchantAccessEntity? merchantAccess = item.Adapt<MerchantAccessEntity>();
                    await _merchantRepository.InsertAsync(merchantAccess, cancellation);
                }
            }

            return merchantAccesses.Adapt<List<PosViewModel>>();
        }
        return new List<PosViewModel>();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="registerOrder"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Result<Guid>> RegisterOrderAsync(RegisterOrderDto registerOrder,
        CancellationToken cancellation = default)
    {
        #region AppendInDatabase

        OrderEntity order = new();
        List<OrderDetailEntity>? detail = JsonConvert.DeserializeObject
            <List<OrderDetailEntity>>(registerOrder.OrderDetail!);
        order = registerOrder.Adapt<OrderEntity>();
        order.Amount = detail!.Sum(s => s.Fee);
        order.TotalPrice = order.Amount + order.ValueAddedTax;
        await _orderRepository.InsertAsync(order, cancellation);
        detail!.ForEach(f =>
        {
            f.OrderId = order.Id;
        });
        await _orderDetailRepository.InsertAsync(detail, cancellation);
        #endregion
        #region Tracking



        Result<string> accessCode = await GetPosAccessTokenAsync(registerOrder.TerminalCode!, cancellation);

        if (accessCode.IsSuccess is false)
        {
            return Result<Guid>.Fail(data: Guid.Empty);
        }


        PaymentInformationRequestModel request = new()
        {
            AccessToken = accessCode.Data,
            terminalCode = registerOrder.TerminalCode,
            payment = new()
            {
                new Payment()
            {
                amount = order.TotalPrice,
                barcode = registerOrder.WalletBarcode
            }
            }
        };



        TrackingCodeResponseModel? trackingCode =
            await _taraService.GetTrackingCodeAsync(request);

        if (trackingCode is null)
        {
            return Result<Guid>.Fail(data: Guid.Empty);
        }



        TypeAdapterConfig config = new();
        config.NewConfig<TrackingCodeResponseModel, OrderTrackingEntity>()
            .Map(a => a.Code, b => b.code)
            .Map(a => a.Mobile, b => b.mobile)
            .Map(a => a.DoTime, b => b.doTime)
            .Map(a => a.Message, b => b.message)
            .Map(a => a.TraceNumber, b => b.traceNumber).Compile();
        OrderTrackingEntity orderTracking = trackingCode.Adapt<OrderTrackingEntity>(config);
        orderTracking.OrderId = order.Id;
        await _orderTrackingRepository.InsertAsync(orderTracking);

        #endregion
        return Result<Guid>.Success(order.Id);

    }

    private async Task<Result<string>> GetPosAccessTokenAsync(string terminal, CancellationToken cancellation = default)
    {
        if (string.IsNullOrWhiteSpace(terminal))
        {
            return Result<string>.Fail(string.Empty);
        }

        MerchantAccessEntity? merchantAccess = await _merchantRepository.GetAsync(g => g.terminalCode == terminal, cancellation);

        if (merchantAccess is null)
        {
            return Result<string>.Fail(string.Empty);
        }

        return Result<string>.Success(merchantAccess.accessCode);
    }

    public async Task<Result<Guid>> PurchaseRequestTaraAsync
        (Guid orderId, string terminal, CancellationToken cancellation = default)
    {
        if (orderId == Guid.Empty || string.IsNullOrEmpty(terminal))
        {
            return Result<Guid>.Fail(Guid.Empty, Message.InvalidPurchase);
        }
        IQueryable<OrderEntity> query = await _orderRepository.GetByQueryAsync();

        OrderEntity? order = await query.Include(i => i.OrderTracking).
            Include(i => i.Details).SingleOrDefaultAsync(
            order => order.Id == orderId, cancellation);
        if (order is null)
        {
            return Result<Guid>.Fail(Guid.Empty, Message.InvalidPurchase);
        }
        Result<PurchaseResponseModel> resultPurchaseRequestTara
            = await PurchaseRequestTaraAsync(order, terminal, cancellation);

        if (resultPurchaseRequestTara is null || resultPurchaseRequestTara.IsSuccess == false)
        {
            return Result<Guid>.Fail(Guid.Empty, Message.ProblemConnectingTara);
        }
        TaraPurchaseEntity taraPurchase = new();

        PurchaseResponseModel purchase
            = resultPurchaseRequestTara.Data!;
        taraPurchase = purchase.Adapt<TaraPurchaseEntity>();
        taraPurchase.OrderId = orderId;
        taraPurchase.terminal = terminal;
        taraPurchase.PurchaseType=Domain.Enum.PurchaseEnum.TemporaryRegistration;
        await _taraPurchaseRepository.InsertAsync(taraPurchase);

        return Result<Guid>.Success(orderId);
    }

    private async Task<Result<PurchaseResponseModel>> PurchaseRequestTaraAsync
        (OrderEntity? order, string terminal, CancellationToken cancellation = default)
    {
        Result<string> accessCode = await GetPosAccessTokenAsync(terminal!, cancellation);

        if (accessCode.IsSuccess is false)
        {
            return Result<PurchaseResponseModel>.Fail(null);
        }
        #region Config
        TypeAdapterConfig configDetail = new();
        configDetail.NewConfig<OrderDetailEntity, InvoiceDataItem>()
            .Map(a => a.code, b => b.ProductCode)
            .Map(a => a.count, b => b.Count)
            .Map(a => a.fee, b => b.Fee)
            .Map(a => a.group, b => b.Group)
            .Map(a => a.groupTitle, b => b.GroupTitle)
            .Map(a => a.made, b => b.Made)
            .Map(a => a.name, b => b.ProductName)
            .Map(a => a.unit, b => b.Unit).Compile();


        TypeAdapterConfig config = new();
        config.NewConfig<OrderEntity, InvoiceData>()
            .Map(a => a.vat, b => b.ValueAddedTax)
            .Map(a => a.invoiceNumber, b => b.InvoiceNumber)
            .Map(a => a.totalPrice, b => b.Details!.Sum(a => a.Fee))
            .Compile();
        #endregion
        List<InvoiceDataItem> invoiceDataItems =
         order!.Details.Adapt<List<InvoiceDataItem>>(configDetail);

        InvoiceData data = order.Adapt<InvoiceData>(config);
        data.items = invoiceDataItems;

        PurchaseRequestModel purchaseRequest = new();
        purchaseRequest.CashDeskToken = accessCode.Data;
        purchaseRequest.invoiceData = data;
        purchaseRequest.traceNumber = order.OrderTracking!.TraceNumber;
        purchaseRequest.invoiceNumber = order.InvoiceNumber;
        purchaseRequest.amount = data.totalPrice + data.vat;


        PurchaseResponseModel? purchase = await _taraService.PurchaseAsync(purchaseRequest);
        if (purchase is null)
        {
            return Result<PurchaseResponseModel>.Fail(null);
        }

        return Result<PurchaseResponseModel>.Success(purchase);
    }

    public async Task<Result> VerifyPurchaseTaraAsync(Guid orderId, string terminal, 
        CancellationToken cancellation = default)
    {
        if (orderId == Guid.Empty || string.IsNullOrEmpty(terminal))
        {
            return Result<Guid>.Fail(Guid.Empty, Message.InternalError);
        }
        Result<string> resultToken=await  GetPosAccessTokenAsync(terminal,cancellation);
        if (!resultToken.IsSuccess)
        {
            return Result<Guid>.Fail(Guid.Empty, Message.InternalError);
        }
        TaraPurchaseEntity? taraPurchase = await _taraPurchaseRepository
          .GetAsync(s => s.OrderId == orderId, cancellation);
        if (taraPurchase is null) 
        {
            return Result<Guid>.Fail(Guid.Empty, Message.InternalError);
        }
        VerifyPurchaseRequestModel request = new()
        {
            CashDeskToken = resultToken.Data,
            traceNumber=taraPurchase.traceNumber
        };

        VerifyPurchaseResponseModel? verifyPurchase = await _taraService.VerifyPurchaseAsync(request);
        if (verifyPurchase != null && verifyPurchase!.success is true) 
        {

            taraPurchase.Adapt(verifyPurchase);

            taraPurchase.PurchaseType = Domain.Enum.PurchaseEnum.PurchaseConfirmation;
            await _taraPurchaseRepository.UpdateAsync(taraPurchase);
            return Result.Success();
        }

        return Result<Guid>.Fail(Guid.Empty, Message.InternalError);
    }

    public async Task<Result> ReversePurchaseTaraAsync
        (Guid orderId, string terminal, CancellationToken cancellation = default)
    {
        if (orderId == Guid.Empty || string.IsNullOrEmpty(terminal))
        {
            return Result<Guid>.Fail(Guid.Empty, Message.InternalError);
        }
        Result<string> resultToken = await GetPosAccessTokenAsync(terminal, cancellation);
        if (!resultToken.IsSuccess)
        {
            return Result<Guid>.Fail(Guid.Empty, Message.InternalError);
        }
        TaraPurchaseEntity? taraPurchase = await _taraPurchaseRepository
          .GetAsync(s => s.OrderId == orderId, cancellation);
        if (taraPurchase is null)
        {
            return Result<Guid>.Fail(Guid.Empty, Message.InternalError);
        }
        ReversePurchaseRequestModel request = new()
        {
            CashDeskToken = resultToken.Data,
            traceNumber = taraPurchase.traceNumber
        };

        ReversePurchaseResponseModel? verifyPurchase = await _taraService.ReversePurchaseAsync(request);
        if (verifyPurchase != null && verifyPurchase!.success is true)
        {

            taraPurchase.Adapt(verifyPurchase);

            taraPurchase.PurchaseType = Domain.Enum.PurchaseEnum.CancelPurchase;
            await _taraPurchaseRepository.UpdateAsync(taraPurchase);
            return Result.Success();
        }

        return Result<Guid>.Fail(Guid.Empty, Message.InternalError);
    }
}
