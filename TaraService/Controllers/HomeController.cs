using Application.Common;
using Application.Common.Extensions;
using Application.Dto.Order;
using Application.Services.Pos;
using Application.ViewModel.PosViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Tara.Models.Home;
namespace Tara.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPos _pos;
    public HomeController(ILogger<HomeController> logger, IPos pos)
    {
        _pos = pos;
        _logger = logger;
    }

    [HttpGet]
   
    public async Task<IActionResult> Index()
    {
        List<PosViewModel> pageModel = await _pos.GetPosesAsync();
        return View(pageModel);
    }

    [HttpGet]
    [Route("/Pos")]
    public async Task<IActionResult>
        Pos(string terminal, CancellationToken cancellation = default)
    {

        ModelState.IsValid

        List<MerchandiseGroupViewModel> groups;
        var pageModel = (Result<List<MerchandiseGroupViewModel>>)await _pos.GetMerchandiseGroupsAsync(terminal, cancellation);

        if (pageModel.IsSuccess is false)
        {

            groups = new();
        }
        else
        {
            groups = pageModel.Data;
        }
        ViewBag.Terminal = terminal;
        ViewBag.Groups = groups;
        ViewBag.CreateTime = DateTime.Now.PersainDateTime();
        return View();
    }


    [HttpPost]
    [Route("/RegisterOrder")]
    public async Task<JsonResult> RegisterOrder([FromBody] OrderInputModel order)
    {

        TypeAdapterConfig config = new();
        config.NewConfig<OrderInputModel, RegisterOrderDto>()
            .Map(a => a.CustomerFullName, b => b.fullName)
            .Map(a => a.CustomerPhoneNumber, b => b.phoneNumber)
            .Map(a => a.OrderDate, b => b.dateTime)
            .Map(a => a.WalletBarcode, b => b.barcode)
            .Map(a => a.TerminalCode, b => b.terminal)
            .Map(a => a.OrderDetail, b => b.detail).Compile();
        Result<Guid> result = await _pos.RegisterOrderAsync
       (order.Adapt<RegisterOrderDto>(config));
        return Json(new
        {
            result.IsSuccess,
            result.Data
        });



    }


    [HttpGet]
    [Route("/PurchaseRequestTara")]
    public async Task<IActionResult> PurchaseRequestTara(string terminal,Guid orderId)
    {
        Result<Guid> result = await _pos.PurchaseRequestTaraAsync(orderId, terminal);
        if (!result.IsSuccess)
        {
            ViewBag.Error = result.Message;

        }
        ViewBag.Terminal= terminal;
        return View(result.Data);
    }



    [HttpPost]
    public async Task<JsonResult> VerifyPurchase
        ([FromBody]PurchaseInputModel purchase)
    {

        Result result = await _pos.VerifyPurchaseTaraAsync(purchase.orderId,
            purchase.terminal!, default);
        return Json(result);
    }
    [HttpPost]
    public async Task<IActionResult> ReversePurchase([FromBody] PurchaseInputModel purchase)
    {
        Result result = await _pos.ReversePurchaseTaraAsync(purchase.orderId,
        purchase.terminal!, default);
        return Json(result);
    }

}
