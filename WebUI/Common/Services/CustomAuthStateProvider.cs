using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using ModelLibrary.Account;

namespace WebUI.Services;

public class AppStateProvider : AuthenticationStateProvider
{
    private readonly ApiRequest _apiRequest;
    private readonly AuthenticationState _annoymous = new(new());

    public AppStateProvider(ApiRequest apiRequest)
        => _apiRequest = apiRequest;

    public override async Task<AuthenticationState>
        GetAuthenticationStateAsync()
    {
        var request = await _apiRequest
            .GetAsync<RequestResponse<UserProfile>?>(
                ApiRoutes.Account.GetIdentity, CancellationToken.None);

        if (request is null || request.Response == null) return _annoymous;

        var claims = new Claim[]
        {
            new(ClaimTypes.Name, request.Response.Email),
            new(ClaimTypes.Role, request.Response.Role)
        };

        return CreateIdenity(claims);
    }

    public async Task<AuthenticationState> RefreshIdentity()
    {
        var identity = await GetAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(identity));
        return identity;
    }

    public async Task LogOut()
    {
        var request = await _apiRequest.GetAsync<bool>(
            ApiRoutes.Account.Logout, CancellationToken.None);
        NotifyAuthenticationStateChanged(Task.FromResult(_annoymous));
    }

    private AuthenticationState CreateIdenity(IEnumerable<Claim> claims)
    {
        if (claims is null) return _annoymous;

        var authenticatedUser = new ClaimsPrincipal(
            new ClaimsIdentity(claims, "Auth"));
        var authState = new AuthenticationState(authenticatedUser);

        return authState;
    }
}
