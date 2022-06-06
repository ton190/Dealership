using Microsoft.AspNetCore.Components.Web;
using ModelLibrary.CarRecords;

namespace WebUI.Pages.Administration.CarRecordOrders;

public class IndexBase : ComponentBase
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected List<CarRecordOrderDto>? Orders { get; set; }
    protected List<OrderBox> OrderBoxes { get; set; } = new();

    protected override async Task OnInitializedAsync() => await LoadData();

    protected async Task LoadData()
    {
        Orders = (await ApiRequest
            .GetAsync<RequestResponse<List<CarRecordOrderDto>>>(
                ApiRoutes.CarRecordOrders.GetAll))!.Response;
        Console.WriteLine(Orders?.Count());
        StateHasChanged();
    }

    protected void Update(CarRecordOrderDto order)
    {
    }

    private void SetBoxHandlers()
    {
        OrderBoxes = new();
        if (Orders is null) return;

        foreach (var order in Orders)
        {
            var box = new OrderBox();
            box.Order = order;
            box.Action = (e) => Update(order);
            OrderBoxes.Add(box);
        }
    }

    protected class OrderBox
    {
        public CarRecordOrderDto? Order { get; set; }
        public Action<MouseEventArgs> Action { get; set; } = e => { };
    }
}
