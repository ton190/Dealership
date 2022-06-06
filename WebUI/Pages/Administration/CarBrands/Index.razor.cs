using Microsoft.AspNetCore.Components.Web;
using ModelLibrary.CarBrands;
using WebUI.Pages.Administration.CarBrands;

namespace WebUI.Pages.Administration;

public class IndexBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected BrandAction BrandAction { get; set; } = null!;
    protected List<CarBrandDto>? CarBrands;
    protected List<BrandBox> BrandBoxes { get; set; } = new();

    protected override async Task OnInitializedAsync() => await LoadData();

    protected async Task LoadData()
    {
        CarBrands = (await ApiRequest
            .GetAsync<RequestResponse<List<CarBrandDto>>>(
                ApiRoutes.CarBrands.GetAll))!.Response;
        SetBoxHandlers();
        StateHasChanged();
    }

    private void SetBoxHandlers()
    {
        BrandBoxes = new();
        if(CarBrands is null) return;

        foreach (var brand in CarBrands)
        {
            var box = new BrandBox();
            box.Brand = brand;
            box.Action = (e) => BrandAction.Update(brand);
            BrandBoxes.Add(box);
        }
    }

    protected class BrandBox
    {
        public CarBrandDto? Brand { get; set; }
        public Action<MouseEventArgs> Action { get; set; } = e => { };
    }
}
