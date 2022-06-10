using Microsoft.AspNetCore.Components.Web.Virtualization;
using ModelLibrary.CarBrands;
using ModelLibrary.CarRecords;

namespace WebUI.Pages.Administration.CarRecords;

public class IndexBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected RecordActionBase RecordAction { get; set; } = null!;
    protected List<CarBrandDto> CarBrands { get; set; } = new();
    protected Virtualize<CarRecordDto> Container { get; set; } = null!;
    protected string SearchString = string.Empty;
    private int maxListSize = 0;

    protected override async Task OnInitializedAsync()
    {
        await GetBrands();
    }

    protected async ValueTask<ItemsProviderResult<CarRecordDto>>
        LoadRecords(ItemsProviderRequest request)
    {
        var query = ApiRoutes.CarRecords.GetAll;
        query += $"?index={request.StartIndex}";
        query += $"&size={request.Count}";
        if (SearchString != "") query += $"&search={SearchString}";

        var list = (await ApiRequest
                .GetAsync<RequestResponse<ListQuery<CarRecordDto>>>(
                    query, request.CancellationToken))!.Response;

        var records = list is null ? new() : list;

        var baseSize = request.StartIndex + request.Count + 10;
        maxListSize = Math.Max(
            maxListSize, Math.Min(records.TotalItems, baseSize));

        return new ItemsProviderResult<CarRecordDto>(
            records.Items, maxListSize);
    }

    protected async Task GetBrands()
    {
        var request = await ApiRequest
            .GetAsync<RequestResponse<ListQuery<CarBrandDto>>>(
                ApiRoutes.CarBrands.GetAll, CancellationToken.None);

        if (request != null && request.Success && request.Response != null)
            CarBrands = request.Response.Items;
    }

    protected async Task Refresh()
    {
        maxListSize = 0;
        await Container.RefreshDataAsync();
    }
}
