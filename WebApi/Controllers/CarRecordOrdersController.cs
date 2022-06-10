using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Basic;
using ModelLibrary.CarRecords;
using Shared;

namespace WebApi.Controllers;

public class CarRecordOrdersController : BasicControllerBase
{
    [HttpPost(ApiRoutes.CarRecordOrders.Create)]
    public async Task<IActionResult> Create([FromBody] CarRecordSearchDto dto)
        => await BasicAction<CreateCarRecordOrderModel, string>(new(dto));

    [HttpPut(ApiRoutes.CarRecordOrders.Update)]
    public async Task<IActionResult> Update([FromBody] CarRecordOrderDto dto)
        => await BasicAction<UpdateCarRecordOrderModel, bool>(new(dto));

    [HttpDelete(ApiRoutes.CarRecordOrders.Remove + "{id:int}")]
    public async Task<IActionResult> Remove(int id)
        => await BasicAction<RemoveCarRecordOrderModel, bool>(new(id));

    [HttpGet(ApiRoutes.CarRecordOrders.GetAll)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int index = 1,
        [FromQuery] int size = 0,
        [FromQuery] string? search = null,
        [FromQuery] bool? paid = null)
        => await BasicAction<GetAllCarRecordOrdersModel,
        ListQuery<CarRecordOrderDto>>(new(true, index, size, search, paid));

    [HttpGet(ApiRoutes.CarRecordOrders.GetByToken)]
    public async Task<IActionResult> GetCarRecordOrderByToken(
        [FromQuery] string token)
        => await BasicAction<GetCarRecordOrderByTokenModel,
            CarRecordOrderDto>(new(token));

    [HttpGet(ApiRoutes.CarRecordOrders.GetStatistics)]
    public async Task<IActionResult> GetCarRecordOrdersStatistic()
        => await BasicAction<GetCarRecordOrdersStatisticsModel,
            CarRecordOrdersStatisticsDto>(new());
}
