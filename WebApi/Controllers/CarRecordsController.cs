using Microsoft.AspNetCore.Mvc;
using ModelLibrary.CarRecords;
using Shared;

namespace WebApi.Controllers;

public class CarRecordsController : BasicControllerBase
{
    [HttpPost(ApiRoutes.CarRecords.Create)]
    public async Task<IActionResult> Create([FromBody] CarRecordDto dto)
        => Ok(await BasicAction<CreateCarRecordModel, bool>(new(dto), new()));

    [HttpPut(ApiRoutes.CarRecords.Update)]
    public async Task<IActionResult> Update([FromBody] CarRecordDto dto)
        => Ok(await BasicAction<UpdateCarRecordModel, bool>(new(dto), new()));

    [HttpDelete(ApiRoutes.CarRecords.Remove + "{id:int}")]
    public async Task<IActionResult> Remove(int id)
        => Ok(await BasicAction<RemoveCarRecordModel, bool>(new(id), new()));

    [HttpGet(ApiRoutes.CarRecords.GetAll)]
    public async Task<IActionResult> GetAllCarBrands()
        => Ok(await BasicGetAction<GetAllCarRecordsModel, List<CarRecordDto>>(
            new(), MemoryCacheNames.CarRecords));
}