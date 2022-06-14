using ModelLibrary.Brands;

namespace WebUI.Pages.Administration.Brands;

public class IndexBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected BrandAction BrandAction { get; set; } = null!;
    protected List<BrandDto>? Brands;

    protected override async Task OnInitializedAsync() => await LoadData();

    protected async Task LoadData()
    {
        var request = await ApiRequest
            .GetAsync<RequestResponse<ListQuery<BrandDto>>>(
                ApiRoutes.Brands.GetAll);
        Brands = request?.Response is null ? new() : request.Response.Items;
    }
}
