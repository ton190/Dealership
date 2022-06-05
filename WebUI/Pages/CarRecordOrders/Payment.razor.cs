namespace WebUI.Pages.CarRecordOrders;

public class PaymentBase : ComponentBase
{
    [Inject] NavigationManager NavManager { get; set; } = null!;
    [Parameter][SupplyParameterFromQuery(Name = "token")]
    public string? Token { get; set; }

    protected void MakeAPayment()
    {
        if(Token != null)
            NavManager.NavigateTo(
                UIRoutes.CarRecordOrders.SearchResult + "?token="+Token);
    }
}
