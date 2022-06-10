using ModelLibrary.CarRecords;

namespace WebUI.Pages.Administration;

public class IndexBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected CarRecordOrdersStatisticsDto? OrdersStatistics { get; set; }
    protected CarRecordsStatisticsDto? RecordsStatistics { get; set; }

    protected override async Task OnInitializedAsync() => await LoadData();

    protected async Task LoadData()
    {
        OrdersStatistics = (await ApiRequest
            .GetAsync<RequestResponse<CarRecordOrdersStatisticsDto>>(
                ApiRoutes.CarRecordOrders.GetStatistics,
                CancellationToken.None))!.Response;
        StateHasChanged();

        RecordsStatistics = (await ApiRequest
            .GetAsync<RequestResponse<CarRecordsStatisticsDto>>(
                ApiRoutes.CarRecords.GetStatistics,
                CancellationToken.None))!.Response;
        StateHasChanged();
    }
}
