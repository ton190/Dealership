using Microsoft.AspNetCore.Components.Web.Virtualization;
using ModelLibrary.Brands;
using ModelLibrary.Records;

namespace WebUI.Pages.Administration.Records;

public class IndexBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected RecordActionBase RecordAction { get; set; } = null!;
    protected List<BrandDto> Brands { get; set; } = new();
    protected Virtualize<RecordDto> Container { get; set; } = null!;
    protected string SearchString = string.Empty;
    private int maxListSize = 0;

    protected override async Task OnInitializedAsync()
    {
        await GetBrands();
    }

    protected async ValueTask<ItemsProviderResult<RecordDto>>
        LoadRecords(ItemsProviderRequest request)
    {
        var query = ApiRoutes.Records.GetAll;
        query += $"?index={request.StartIndex}";
        query += $"&size={request.Count}";
        if (SearchString != "") query += $"&search={SearchString}";

        var list = (await ApiRequest
                .GetAsync<RequestResponse<ListQuery<RecordDto>>>(
                    query, request.CancellationToken))!.Response;

        var records = list is null ? new() : list;

        var baseSize = request.StartIndex + request.Count + 10;
        maxListSize = Math.Max(
            maxListSize, Math.Min(records.TotalItems, baseSize));

        return new ItemsProviderResult<RecordDto>(
            records.Items, maxListSize);
    }

    protected async Task GetBrands()
    {
        var request = await ApiRequest
            .GetAsync<RequestResponse<ListQuery<BrandDto>>>(
                ApiRoutes.Brands.GetAll, CancellationToken.None);

        if (request != null && request.Success && request.Response != null)
            Brands = request.Response.Items;
    }

    protected async Task OnSearch(ChangeEventArgs e)
    {
        if (e.Value is null) return;
        SearchString = (string)e.Value;
        await Refresh();
    }

    protected async Task Refresh()
    {
        maxListSize = 0;
        await Container.RefreshDataAsync();
    }
}
