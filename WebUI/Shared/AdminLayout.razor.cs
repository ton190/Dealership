using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;

namespace WebUI.Shared;

public class AdminLayoutBase : LayoutComponentBase, IDisposable
{
    [CascadingParameter]
    [Inject] AuthenticationStateProvider AuthState { get; set; } = null!;
    [Inject] NavigationManager NavManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        NavManager.LocationChanged += OnLocationChanged;
        await AuthUser();
        base.OnInitialized();
    }

    private async void OnLocationChanged(
        object? sender, LocationChangedEventArgs args)
    {
        if(!args.Location.Contains(UIRoutes.Administration.Root)) return;
        await AuthUser();
    }

    private async Task AuthUser()
    {
        var state = await ((AppStateProvider)AuthState).RefreshIdentity();

        if (state?.User?.Identity is not null
            && state.User.Identity.IsAuthenticated) return;

        if (NavManager.ToAbsoluteUri(
            UIRoutes.Administration.Account.Login).ToString()
            == NavManager.Uri) return;

        NavManager.NavigateTo(UIRoutes.Administration.Account.Login);
    }

    public async Task LogOut()
    {
        await ((AppStateProvider)AuthState).LogOut();
        NavManager.NavigateTo("/");
    }

    public void Dispose()
    {
        NavManager.LocationChanged -= OnLocationChanged;
    }
}
