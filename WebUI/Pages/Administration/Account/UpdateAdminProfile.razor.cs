using ModelLibrary.Account;

namespace WebUI.Pages.Administration.Account;

public class UpdateAdminProfileBase : ComponentBase, IDisposable
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected EditContext? EditContext { get; set; }
    protected UserDto? Model { get; set; }
    protected bool status;

    protected override async Task OnInitializedAsync()
    {
        var result = await ApiRequest.GetAsync<RequestResponse<UserDto>>(
            ApiRoutes.Account.GetAdminProfile);
        if (result?.Response is null) return;
        Model = result.Response;
        EditContext = new(Model);
        EditContext.OnFieldChanged += ClearStatus;
    }

    private void ClearStatus(
        object? o, FieldChangedEventArgs e) => status = false;

    protected async Task OnValidSubmit()
    {
        if (EditContext is null || !EditContext.IsModified()) return;
        EditContext.MarkAsUnmodified();
        var result = await ApiRequest.PutAsync<RequestResponse>(
            ApiRoutes.Account.UpdateAdminProfile, EditContext.Model);
        status = true;
    }

    public void Dispose()
    {
        if (EditContext != null) EditContext.OnFieldChanged -= ClearStatus;
    }
}
