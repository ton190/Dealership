using System.Text.Json;
using ModelLibrary.CarRecords;

namespace WebUI.Pages.CarRecordOrders;

public class SearchResultBase : ComponentBase
{
    [Parameter]
    [SupplyParameterFromQuery(Name = "token")]
    public string? Token { get; set; }
    [Inject] public ApiRequest ApiRequest { get; set; } = null!;
    protected CarRecordOrderDto? Order { get; set; }
    protected CarRecordSearchDto? RecordSearch { get; set; }
    protected List<CarRecordDto>? CarRecords { get; set; }
    protected bool PageLoaded { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrWhiteSpace(Token)) return;
        var result = await ApiRequest
            .GetAsync<RequestResponse<CarRecordOrderDto>>(
            ApiRoutes.CarRecordOrders.GetByToken + "?token=" + Token);

        if (result is null ||
            !result.Success ||
            result.Response?.SearchResult is null ||
            result.Response?.RecordSearch is null)
        {
            PageLoaded = true;
            return;
        }

        Order = result.Response;
        CarRecords = JsonSerializer.Deserialize<List<CarRecordDto>>(
            Order.SearchResult);
        RecordSearch = JsonSerializer.Deserialize<CarRecordSearchDto>(
            Order.RecordSearch);

        PageLoaded = true;
    }
}
