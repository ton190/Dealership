using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Basic;
using ModelLibrary.CarRecords;
using Shared;

namespace WebApi.Controllers;

public class CarRecordsController : BasicControllerBase
{
    [HttpPost(ApiRoutes.CarRecords.Create)]
    public async Task<IActionResult> Create([FromBody] CarRecordDto dto)
        => await BasicAction<CreateCarRecordModel, bool>(new(dto));

    [HttpPut(ApiRoutes.CarRecords.Update)]
    public async Task<IActionResult> Update([FromBody] CarRecordDto dto)
        => await BasicAction<UpdateCarRecordModel, bool>(new(dto));

    [HttpDelete(ApiRoutes.CarRecords.Remove + "{id:int}")]
    public async Task<IActionResult> Remove(int id)
        => await BasicAction<RemoveCarRecordModel, bool>(new(id));

    [HttpGet(ApiRoutes.CarRecords.GetStatistics)]
    public async Task<IActionResult> GetCarRecordsStatistic()
        => await BasicAction<GetCarRecordsStatisticsModel,
            CarRecordsStatisticsDto>(new());

    [HttpGet(ApiRoutes.CarRecords.GetAll)]
    public async Task<IActionResult> GetAllCarBrands(
        [FromQuery] int index = 1,
        [FromQuery] int size = 0,
        [FromQuery] string? search = null)
        => await BasicGetAction<GetAllCarRecordsModel,
            ListQuery<CarRecordDto>>(new(true, index, size, search));
}
