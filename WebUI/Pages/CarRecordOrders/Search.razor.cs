using System.Text.RegularExpressions;
using Blazored.LocalStorage;
using ModelLibrary.CarBrands;
using ModelLibrary.CarRecords;

namespace WebUI.Pages.CarRecordOrders;

public class SearchBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    [Inject] NavigationManager NavManager { get; set; } = null!;
    [Inject] ILocalStorageService LocalStorage { get; set; } = null!;
    protected EditContext EditContext = null!;
    protected List<CarBrandDto>? CarBrands { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var request = await ApiRequest
            .GetAsync<RequestResponse<List<CarBrandDto>>>(
                ApiRoutes.CarBrands.GetAll);

        if (request != null && request.Success && request.Response != null)
            CarBrands = request.Response;

        EditContext = new(new CarRecordSearchDto());
    }

    protected bool OnInput()
    {
        var model = (CarRecordSearchDto)EditContext.Model;
        if (model is null) return false;
        if (model.BusinessName != "" ||
            model.ContactName.FullName != "" ||
            model.Phone.Number != "" ||
            model.BusinessAddress.Address != "") return true;
        return false;
    }

    protected async Task OnSubmit()
    {
        var model = (CarRecordSearchDto)EditContext.Model;
        if(model is null) return;

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
            model.BusinessAddress.Address == "") return;

        var request = await ApiRequest
            .PostAsync<RequestResponse<string>>(
                ApiRoutes.CarRecordOrders.Create, model);

        if (request is null || !request.Success) return;
        NavManager.NavigateTo(
            UIRoutes.CarRecordOrders.Payment + "?token=" + request.Response);
    }
}
