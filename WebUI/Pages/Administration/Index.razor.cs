using ModelLibrary.Orders;

namespace WebUI.Pages.Administration;

public class IndexBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected OrdersStatisticsDto? OrdersStatistics { get; set; }
    protected RecordsStatisticsDto? RecordsStatistics { get; set; }

    protected override async Task OnInitializedAsync() => await LoadData();

    protected async Task LoadData()
    {
        OrdersStatistics = (await ApiRequest
            .GetAsync<RequestResponse<OrdersStatisticsDto>>(
                ApiRoutes.Orders.GetStatistics,
                CancellationToken.None))!.Response;
        StateHasChanged();

        RecordsStatistics = (await ApiRequest
            .GetAsync<RequestResponse<RecordsStatisticsDto>>(
                ApiRoutes.Records.GetStatistics,
                CancellationToken.None))!.Response;
        StateHasChanged();
    }
}
