using Microsoft.AspNetCore.Mvc;
using ModelLibrary.CarBrands;
using Shared;

namespace WebApi.Controllers;

public class CarBrandsController : BasicControllerBase
{
    [HttpPost(ApiRoutes.CarBrands.Create)]
    public async Task<IActionResult> Create([FromBody] CarBrandDto dto)
        => await BasicAction<CreateCarBrandModel, bool>(new(dto), new());

    [HttpPut(ApiRoutes.CarBrands.Update)]
    public async Task<IActionResult> Update([FromBody] CarBrandDto dto)
        => await BasicAction<UpdateCarBrandModel, bool>(new(dto), new());

    [HttpDelete(ApiRoutes.CarBrands.Remove + "{id:int}")]
    public async Task<IActionResult> Remove(int id)
        => await BasicAction<RemoveCarBrandModel, bool>(new(id), new());

    [HttpGet(ApiRoutes.CarBrands.GetAll)]
    public async Task<IActionResult> GetAllCarBrands()
    {
        var result = await BasicGetAction<
            GetAllCarBrandsModel, List<CarBrandDto>>(
                new(), MemoryCacheNames.CarBrands);
        return result != null ? Ok(result) : BadRequest();
    }
}
