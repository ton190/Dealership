using System.Text.RegularExpressions;
using Blazored.LocalStorage;
using ModelLibrary.Brands;
using ModelLibrary.Orders;

namespace WebUI.Pages.Orders;

public class SearchBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    [Inject] NavigationManager NavManager { get; set; } = null!;
    [Inject] ILocalStorageService LocalStorage { get; set; } = null!;
    protected EditContext EditContext = null!;
    protected List<BrandDto>? Brands { get; set; }
    protected string Error { get; set; } = "No Errors";

    protected override async Task OnInitializedAsync()
    {
        var request = await ApiRequest
            .GetAsync<RequestResponse<ListQuery<BrandDto>>>(
                ApiRoutes.Brands.GetAll, CancellationToken.None);

        if (request != null && request.Success && request.Response != null)
            Brands = request.Response.Items;

        EditContext = new(new RecordSearchDto());
    }

    protected bool OnInput()
    {
        var model = (RecordSearchDto)EditContext.Model;
        if (model is null) return false;
        if (model.BusinessName != "" ||
            model.ContactName.FullName != "" ||
            model.Phone.Number != "" ||
            model.BusinessAddress.FullAddress != "") return true;
        return false;
    }

    protected async Task OnSubmit()
    {
        var model = (RecordSearchDto)EditContext.Model;
        if (model is null) return;

        var regexObj = new Regex(@"[^\d]");
        var numberString = regexObj.Replace(model.Phone.Number, "");
        if (numberString.Length > 0)
        {
            var firstNumber = numberString.Substring(0, 1);
            if (firstNumber == "1")
                numberString = numberString.Remove(0, 1);
        }
        model.Phone.Number = numberString;

        model.BusinessAddress.PostalCode = model.BusinessAddress.PostalCode
            .ToUpper().Replace("[^A-Z0-9]", "").Replace(" ", "");

        if (!EditContext.Validate()) return;

        if (model.BusinessName == "" &&
            model.ContactName.FullName == "" &&
            model.Phone.Number == "" &&
            model.BusinessAddress.FullAddress == "") return;

        var request = await ApiRequest
            .PostAsync<RequestResponse<string>>(
                ApiRoutes.Orders.Create, model);

        if (request is null || !request.Success || request.Response == null)
            NavManager.NavigateTo(UIRoutes.Error);
        else
            NavManager.NavigateTo(request.Response);

    }
}
