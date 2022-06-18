using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Orders;
using Shared;

namespace WebApi.Controllers;

public class OrdersController : BasicControllerBase
{
    private readonly IPaymentService _paymentService;

    public OrdersController(IPaymentService paymentService)
        => _paymentService = paymentService;

    [HttpPost(ApiRoutes.Orders.Create)]
    public async Task<IActionResult> Create([FromBody] RecordSearchDto dto,
        CancellationToken ct = default(CancellationToken))
        => await BasicAction<CreateOrderModel>(new(dto), null, ct);

    [Authorize(Roles = "admin")]
    [HttpGet(ApiRoutes.Orders.GetAll)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int index = 0,
        [FromQuery] int size = 0,
        [FromQuery] string? search = null)
        => await BasicAction<GetAllOrdersModel>(
            new(true, index, size, search));

    [HttpGet(ApiRoutes.Orders.GetOrder)]
    public async Task<IActionResult> GetOrder(
        [FromQuery] string session_id)
        => await BasicAction<GetOrderModel>(new(session_id));

    [Authorize(Roles = "admin")]
    [HttpGet(ApiRoutes.Orders.GetStatistics)]
    public async Task<IActionResult> GetStatistic()
        => await BasicAction<OrdersStatisticsModel>(new());
}
