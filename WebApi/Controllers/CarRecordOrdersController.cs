using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Basic;
using ModelLibrary.CarRecords;
using Shared;

namespace WebApi.Controllers;

public class CarRecordOrdersController : BasicControllerBase
{
    [HttpPost(ApiRoutes.CarRecordOrders.Create)]
    public async Task<IActionResult> Create([FromBody] CarRecordSearchDto dto)
    {
        var request = await BasicGetAction<GetAllCarRecordsModel,
            List<CarRecordDto>>(new(), MemoryCacheNames.CarRecords);

        var dbList = await GetCarRecords();
        if (dbList is null) return BadRequest();

        return await BasicAction<CreateCarRecordOrderModel, string>(
            new(dto, dbList));
    }

    [HttpGet(ApiRoutes.CarRecordOrders.GetByToken)]
    public async Task<IActionResult> GetCarRecordOrderByToken(
        [FromQuery] string token)
        => await BasicAction<GetCarRecordOrderByTokenModel,
            CarRecordOrderDto>(new(token));

    [HttpGet(ApiRoutes.CarRecordOrders.GetAll)]
    public async Task<IActionResult> GetAllCarRecordOrders()
        => await BasicAction<
            GetAllCarRecordOrdersModel, List<CarRecordOrderDto>>(new());

    //Get Car Records
    private async Task<List<CarRecordDto>?> GetCarRecords()
    {
        var request = await BasicGetAction<GetAllCarRecordsModel,
            List<CarRecordDto>>(new(), MemoryCacheNames.CarRecords);

        if (request is null) return null;
        var response = (RequestResponse<List<CarRecordDto>>)request;
        return response.Response;
    }
}
