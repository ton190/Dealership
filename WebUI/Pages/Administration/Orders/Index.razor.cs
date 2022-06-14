using Microsoft.AspNetCore.Components.Web.Virtualization;
using ModelLibrary.Orders;

namespace WebUI.Pages.Administration.Orders;

public class IndexBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected Virtualize<OrderDto> Container { get; set; } = null!;
    protected OrderActionBase OrderAction { get; set; } = null!;
    protected string SearchString = string.Empty;
    private int maxListSize = 0;

    protected async ValueTask<ItemsProviderResult<OrderDto>>
        LoadOrders(ItemsProviderRequest request)
    {
        var query = ApiRoutes.Orders.GetAll;
        query += $"?index={request.StartIndex}";
        query += $"&size={request.Count}";
        if (SearchString != "") query += $"&search={SearchString}";

        var list = (await ApiRequest
                .GetAsync<RequestResponse<ListQuery<OrderDto>>>(
                    query, request.CancellationToken))!.Response;

        var orders = list is null ? new() : list;

        var baseSize = request.StartIndex + request.Count + 10;
        maxListSize = Math.Max(
            maxListSize, Math.Min(orders.TotalItems, baseSize));

        return new ItemsProviderResult<OrderDto>(
            orders.Items, maxListSize);
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
        StateHasChanged();
    }

    protected void OpenAction(OrderDto dto)
    {
        OrderAction.SetModel(dto);
    }
}
