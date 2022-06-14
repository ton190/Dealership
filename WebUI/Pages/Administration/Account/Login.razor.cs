using ModelLibrary.Account;

namespace WebUI.Pages.Administration.Account;

public class LoginBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    [Inject] NavigationManager NavManager { get; set; } = null!;
    protected LoginModel Model { get; set; } = new();
    protected EditContext? EditContext { get; set; }
    protected ValidationMessageStore? _messagesStore { get; set; }
    protected bool flag;

    protected override void OnInitialized()
    {
        EditContext = new EditContext(Model);
        _messagesStore = new(EditContext);
        EditContext.OnValidationRequested += (s, e) => _messagesStore.Clear();
        EditContext.OnFieldChanged += (s, e)
            => _messagesStore.Clear(e.FieldIdentifier);
    }

    protected async Task OnSubmit()
    {
        if (flag || EditContext is null || _messagesStore is null) return;
        if (!EditContext.Validate()) return;
        flag = true;
        var result = await ApiRequest.PostAsync<RequestResponse>(
            ApiRoutes.Account.Login, Model);
        flag = false;
        if (result is null) return;

        if (result.Errors != null)
        {
            _messagesStore.Add(EditContext.Field("Email"), result.Errors);
            EditContext.NotifyValidationStateChanged();
            return;
        }

        NavManager.NavigateTo(UIRoutes.Administration.Index);
    }
}
