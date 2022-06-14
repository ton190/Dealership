using ModelLibrary.Orders;

namespace WebUI.Pages.Administration.Orders;

public class OrderActionBase : ComponentBase
{
    protected OrderDto? Model { get; set; }

    public void SetModel(OrderDto dto)
    {
        Console.WriteLine("Model");
        Model = dto;
        StateHasChanged();
    }

    public void Cancel()
    {
        Model = null;
    }
}
