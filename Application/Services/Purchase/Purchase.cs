﻿using Microsoft.AspNetCore.Http;

namespace Application.Services.Purchase;

public class Purchase : IPurchase
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly ITaraWebService _taraWebService;
    private readonly IRepository<OrderDetailEntity> _orderDetailRepository;
    private readonly IRepository<OrderEntity> _orderRepository;
    private readonly IContext _context;
    public Purchase(ITaraWebService taraWebService,
        IRepository<OrderDetailEntity> orderDetailRepository,
        IRepository<OrderEntity> orderRepository,
        IHttpContextAccessor httpContext,
        IContext context)
    {
        _httpContextAccessor = httpContext;
        _taraWebService = taraWebService;
        _orderDetailRepository = orderDetailRepository;
        _orderRepository = orderRepository;
        _context = context;
    }

    public async Task<List<ProductGroupViewModel>?>
        GetProductGroupsAsync(CancellationToken cancellation = default)
    {
        List<ProductGroupResponseModel>? groups =
            await _taraWebService.GetProductGroupAsync(cancellation)!;
        if (groups is null)
        {
            return null;
        }
        List<ProductGroupViewModel>
            productGroups = groups.Adapt<List<ProductGroupViewModel>>();
        return productGroups;
    }

    public async Task PurchaseRequestAsync
        (OrderDto order, CancellationToken cancellation = default)
    {
        Guid Id = Guid.Empty;
        #region Insert On DataBase
        if (order == null || order.products == null)
        {
            //todo
            return;
        }
        using (var transaction = await _context.BeginTransactionAsync())
        {
            OrderEntity? entity = await RegisterOrderOnDbAsync(order, cancellation);
            if (entity is null)
            {
                return;
            }
            Result resultInsertOrderDetails = await
                RegisterOrderDetailOnDbAsync(order.products, entity.Id, cancellation);
            if (resultInsertOrderDetails.IsSuccess is false)
            {
                transaction.Rollback();
                return;
            }
            await transaction.CommitAsync();
            Id = entity.Id;
        }
        #endregion
        #region TokenPaymentGateway
        TokenPaymentGatewayRequestModel request = new()
        {
            amount = order.amount,
            mobile = order.mobile,
            vat = order.vat,
            orderId = Id.ToString(),
            ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
            taraInvoiceItemList = order.products.Adapt<List<TaraInvoiceItem>>(),
            serviceAmountList = new()
            {
                new ServiceAmount()
                {
                    amount=Convert.ToInt64(order.amount),
                    serviceId=101
                }
            }
        };
        TokenPaymentGatewayResponseModel? getTokenPaymentGateway
            = await _taraWebService.GetTokenPaymentGatewayAsync(request, cancellation)!;
        if (getTokenPaymentGateway == null)
        {
            return;
        }
        #endregion

        #region GoToIpg

        await _taraWebService.GoToIpgPurchaseAsync(new IpgPurchaseRequestModel()
        {
            username=DefaultData.OnlinePurchaseUserName,
            token= getTokenPaymentGateway.token
        });
        #endregion
    }

    private async Task<OrderEntity?> RegisterOrderOnDbAsync
        (OrderDto order, CancellationToken cancellation = default)
    {
        try
        {
            OrderEntity entity = order.Adapt<OrderEntity>();
            entity.Type = Domain.Enum.PurchaseTypeEnum.Online;
            await _orderRepository.InsertAsync(entity);
            return entity;
        }
        catch (Exception ex)
        {
            //Todo log
            return null;
        }
    }

    private async Task<Result> RegisterOrderDetailOnDbAsync
        (List<OrderItemDto> items, Guid OrderId, CancellationToken cancellation = default)
    {


        try
        {
            List<OrderDetailEntity> entities = items.Adapt<List<OrderDetailEntity>>();
            entities.ForEach(entity => entity.OrderId = OrderId);

            await _orderDetailRepository.InsertAsync(entities);
            return Result.Success();
        }
        catch (Exception ex)
        {
            //Todo log
            return Result.Fail();
        }
    }

}
