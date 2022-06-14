using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Account;
using ModelLibrary.Basic;
using Shared;

namespace WebApi.Controllers;

public class AccountController : BasicControllerBase
{
    [HttpPost(ApiRoutes.Account.Login)]
    public async Task<IActionResult> Login([FromBody] LoginModel model,
        CancellationToken ct = default(CancellationToken))
    {
        var result = await Mediator.Send(model, ct);
        if (!result.Success || result.Response is null) return Ok(result);

        await SetIdentity(result.Response);
        return Ok(result);
    }

    [HttpGet(ApiRoutes.Account.Logout)]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok(true);
    }

    [Authorize(Roles = "admin")]
    [HttpGet(ApiRoutes.Account.GetAdminProfile)]
    public async Task<IActionResult> GetAdminProfile(
        CancellationToken ct = default(CancellationToken))
        => await BasicAction<GetAdminProfileModel>(new());

    [Authorize(Roles = "admin")]
    [HttpPut(ApiRoutes.Account.UpdateAdminProfile)]
    public async Task<IActionResult> UpdateAdminProfile([FromBody] UserDto dto,
        CancellationToken ct = default(CancellationToken))
        => await BasicAction<UpdateAdminProfileModel>(new(dto), null, ct);


    [HttpGet(ApiRoutes.Account.GetIdentity)]
    public IActionResult GetUserProfile()
    {
        var user = HttpContext?.User;
        var identity = user?.Identity;

        if (user?.Identity is null || !user.Identity.IsAuthenticated)
            return Ok(new RequestResponse<UserProfile?>(false));


        var profile = CreateProfile(user.Claims);
        if (profile is null)
            return Ok(new RequestResponse<UserProfile?>(false));


        return Ok(new RequestResponse<UserProfile?>(true, null, profile));
    }

    private async Task SetIdentity(IEnumerable<Claim>? claims)
    {
        if (claims is null)
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

        var identity = new ClaimsIdentity(claims,
            CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }

    private UserProfile? CreateProfile(IEnumerable<Claim>? claims)
    {
        if (claims is null) return null;
        var email = claims
            .Where(_ => _.Type == ClaimTypes.Name).FirstOrDefault();
        var role = claims
            .Where(_ => _.Type == ClaimTypes.Role).FirstOrDefault();

        if (email is null || role is null) return null;

        return new UserProfile
        {
            Email = email.Value,
            Role = role.Value,
        };
    }
}
