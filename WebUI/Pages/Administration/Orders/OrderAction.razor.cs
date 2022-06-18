using ModelLibrary.Orders;

namespace WebUI.Pages.Administration.Orders;

public class OrderActionBase : ComponentBase
{
    protected OrderDto? Model { get; set; }

    public void SetModel(OrderDto dto)
    {
        Model = dto;
        StateHasChanged();
    }

    public void Cancel()
    {
        Model = null;
    }
}
