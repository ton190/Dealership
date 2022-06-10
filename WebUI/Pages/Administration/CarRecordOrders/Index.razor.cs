using Microsoft.AspNetCore.Components.Web.Virtualization;
using ModelLibrary.CarRecords;

namespace WebUI.Pages.Administration.CarRecordOrders;

public class IndexBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected CarRecordOrderAction OrderAction { get; set; } = null!;
    protected Virtualize<CarRecordOrderDto> Container { get; set; } = null!;
    protected string Search = string.Empty;
    protected bool? FilterPaied = true;
    private int maxListSize = 0;

    protected async ValueTask<ItemsProviderResult<CarRecordOrderDto>>
        LoadOrders(ItemsProviderRequest request)
    {
        var query = ApiRoutes.CarRecordOrders.GetAll;
        query += $"?index={request.StartIndex}";
        query += $"&size={request.Count}";
        if (Search != "") query += $"&search={Search}";
        if (FilterPaied != null) query += $"&paid={FilterPaied}";

        var list = (await ApiRequest
                .GetAsync<RequestResponse<ListQuery<CarRecordOrderDto>>>(
                    query, request.CancellationToken))!.Response;

        var orders = list is null ? new() : list;

        var baseSize = request.StartIndex + request.Count + 10;
        maxListSize = Math.Max(
            maxListSize, Math.Min(orders.TotalItems, baseSize));

        return new ItemsProviderResult<CarRecordOrderDto>(
            orders.Items, maxListSize);
    }

    protected async Task Refresh()
    {
        maxListSize = 0;
        await Container.RefreshDataAsync();
        StateHasChanged();
    }
    protected async Task Filter(bool? paid)
    {
        FilterPaied = paid;
        await Refresh();
    }
}
