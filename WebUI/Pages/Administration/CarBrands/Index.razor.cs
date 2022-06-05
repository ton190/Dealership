using ModelLibrary.CarBrands;
using WebUI.Pages.Administration.CarBrands;

namespace WebUI.Pages.Administration;

public class IndexBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected BrandAction BrandAction { get; set; } = null!;
    protected List<CarBrandDto>? CarBrands;

    protected override async Task OnInitializedAsync() => await LoadData();

    protected async Task LoadData()
    {
        CarBrands = (await ApiRequest
            .GetAsync<RequestResponse<List<CarBrandDto>>>(
                ApiRoutes.CarBrands.GetAll))!.Response;
        StateHasChanged();
    }
}
