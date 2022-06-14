using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Brands;
using Shared;

namespace WebApi.Controllers;

public class BrandsController : BasicControllerBase
{
    [Authorize(Roles = "admin")]
    [HttpPost(ApiRoutes.Brands.Create)]
    public async Task<IActionResult> Create([FromBody] BrandDto dto,
        CancellationToken ct = default(CancellationToken))
        => await BasicAction<CreateBrandModel>(
            new(dto), new(MemoryCacheNames.Brands), ct);

    [Authorize(Roles = "admin")]
    [HttpPut(ApiRoutes.Brands.Update)]
    public async Task<IActionResult> Update([FromBody] BrandDto dto,
        CancellationToken ct = default(CancellationToken))
        => await BasicAction<UpdateBrandModel>(
            new(dto), new(MemoryCacheNames.Brands), ct);

    [Authorize(Roles = "admin")]
    [HttpDelete(ApiRoutes.Brands.Remove + "{id:int}")]
    public async Task<IActionResult> Remove(int id,
        CancellationToken ct = default(CancellationToken))
        => await BasicAction<RemoveBrandModel>(
            new(id), new(MemoryCacheNames.Brands), ct);

    [HttpGet(ApiRoutes.Brands.GetAll)]
    public async Task<IActionResult> GetAll()
        => await BasicGetAction<GetAllBrandsModel>(
            new(), MemoryCacheNames.Brands);
}
