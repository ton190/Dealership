using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Interfaces;
using Shared;

namespace WebApi.Controllers;

[ApiController]
public class DbValidatorController : ControllerBase
{
    private readonly IDbValidator _dbValidator;

    public DbValidatorController(IDbValidator dbValidator)
        => _dbValidator = dbValidator;

    [HttpGet(ApiRoutes.DbValidator.IsCarBrandNameExists)]
    public async Task<bool> IsCarBrandNameExists(
        [FromQuery]string name, [FromQuery]int id)
        => await _dbValidator.IsCarBrandNameExists(
            name, id, CancellationToken.None);
}
