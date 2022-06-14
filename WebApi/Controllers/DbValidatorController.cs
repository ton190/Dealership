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

    [HttpGet(ApiRoutes.DbValidator.IsBrandNameExists)]
    public async Task<bool> IsBrandNameExists(
        [FromQuery]string name, [FromQuery]int id)
        => await _dbValidator.IsBrandNameExists(
            name, id, CancellationToken.None);

    [HttpGet(ApiRoutes.DbValidator.IsUserEmailExists)]
    public async Task<bool> IsUserEmailExists(
        [FromQuery]string email, [FromQuery]int id)
        => await _dbValidator.IsUserEmailExists(
            email, id, CancellationToken.None);
}
