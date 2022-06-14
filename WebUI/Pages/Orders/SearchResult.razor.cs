using System.Text.Json;
using ModelLibrary.Orders;
using ModelLibrary.Records;

namespace WebUI.Pages.Orders;

public class SearchResultBase : ComponentBase
{
    [Parameter]
    [SupplyParameterFromQuery(Name = "session_id")]
    public string? SessionId { get; set; }
    [Inject] public ApiRequest ApiRequest { get; set; } = null!;
    [Inject] public NavigationManager NavManager {get;set;} = null!;
    protected OrderDto? Order { get; set; }
    protected RecordSearchDto? RecordSearch { get; set; }
    protected List<RecordDto>? Records { get; set; }
    protected bool PageLoaded { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrWhiteSpace(SessionId)) return;
        var result = await ApiRequest
            .GetAsync<RequestResponse<OrderDto>>(
            ApiRoutes.Orders.GetOrder + "?session_id=" + SessionId,
            CancellationToken.None);

        if (result is null ||
            result.Response?.SearchResult is null ||
            result.Response?.RecordSearch is null ||
            !result.Success)
        {
            PageLoaded = true;
            return;
        }

        Order = result.Response;
        Records = JsonSerializer.Deserialize<List<RecordDto>>(
            Order.SearchResult);
        RecordSearch = JsonSerializer.Deserialize<RecordSearchDto>(
            Order.RecordSearch);

        PageLoaded = true;
    }
}
