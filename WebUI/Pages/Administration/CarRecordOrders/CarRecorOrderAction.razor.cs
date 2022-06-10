using ModelLibrary.CarRecords;

namespace WebUI.Pages.Administration.CarRecordOrders;

public class CarRecordOrderAction : CUDAction<CarRecordOrderDto>
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected override string EntityName => "Order";

    protected override async Task OnRemove()
    {
        if(EditContext is null) return;
        var model = (CarRecordOrderDto)EditContext.Model;
        await ApiRequest.DeleteAsync<RequestResponse>(
            ApiRoutes.CarRecordOrders.Remove + model.Id);
        await Refresh();
    }

    protected override async Task OnSubmit()
    {
        if (!EditContext!.Validate()) return;

        await ApiRequest.PutAsync<RequestResponse>(
            ApiRoutes.CarRecordOrders.Update, EditContext.Model);

        await Refresh();
    }
}
