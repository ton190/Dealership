using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Records;
using Shared;

namespace WebApi.Controllers;

public class RecordsController : BasicControllerBase
{
    [Authorize(Roles = "admin")]
    [HttpPost(ApiRoutes.Records.Create)]
    public async Task<IActionResult> Create([FromBody] RecordDto dto,
        CancellationToken ct = default(CancellationToken))
        => await BasicAction<CreateRecordModel>(new(dto), null, ct);

    [Authorize(Roles = "admin")]
    [HttpPut(ApiRoutes.Records.Update)]
    public async Task<IActionResult> Update([FromBody] RecordDto dto,
        CancellationToken ct = default(CancellationToken))
        => await BasicAction<UpdateRecordModel>(new(dto), null, ct);

    [Authorize(Roles = "admin")]
    [HttpDelete(ApiRoutes.Records.Remove + "{id:int}")]
    public async Task<IActionResult> Remove(int id,
        CancellationToken ct = default(CancellationToken))
        => await BasicAction<RemoveRecordModel>(new(id), null, ct);

    [Authorize(Roles = "admin")]
    [HttpGet(ApiRoutes.Records.GetStatistics)]
    public async Task<IActionResult> GetStatistic()
        => await BasicAction<GetRecordsStatisticsModel>(new());

    [Authorize(Roles = "admin")]
    [HttpGet(ApiRoutes.Records.GetAll)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int index = 0,
        [FromQuery] int size = 0,
        [FromQuery] string? search = null)
        => await BasicGetAction<GetAllRecordsModel>(
            new(true, index, size, search));
}
