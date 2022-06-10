using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Basic;
using ModelLibrary.CarBrands;
using Shared;

namespace WebApi.Controllers;

public class CarBrandsController : BasicControllerBase
{
    [HttpPost(ApiRoutes.CarBrands.Create)]
    public async Task<IActionResult> Create([FromBody] CarBrandDto dto)
        => await BasicAction<CreateCarBrandModel, bool>(new(dto));

    [HttpPut(ApiRoutes.CarBrands.Update)]
    public async Task<IActionResult> Update([FromBody] CarBrandDto dto)
        => await BasicAction<UpdateCarBrandModel, bool>(new(dto));

    [HttpDelete(ApiRoutes.CarBrands.Remove + "{id:int}")]
    public async Task<IActionResult> Remove(int id)
        => await BasicAction<RemoveCarBrandModel, bool>(new(id));

    [HttpGet(ApiRoutes.CarBrands.GetAll)]
    public async Task<IActionResult> GetAllCarBrands()
        => await BasicGetAction<GetAllCarBrandsModel,
            ListQuery<CarBrandDto>>(new());
}
