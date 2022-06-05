using ModelLibrary.CarBrands;
using ModelLibrary.CarRecords;

namespace WebUI.Pages.Administration.CarRecords;

public class RecordActionBase : CUDAction<CarRecordDto>
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    [Parameter] public List<CarBrandDto> CarBrands { get; set; } = new();

    protected override string EntityName => "Car Record";
    protected PhoneAction PhoneAction { get; set; } = null!;
    protected ContactNameAction ContactNameAction { get; set; } = null!;

    protected override async Task OnRemove()
    {
        if(UpdateModel is null) return;

        await ApiRequest.DeleteAsync<RequestResponse>(
            ApiRoutes.CarRecords.Remove + UpdateModel!.Id);
        await Refresh();
    }

    protected override async Task OnSubmit()
    {
        if (EditContext is null) return;
        var model = (CarRecordDto)EditContext.Model;

        model.BusinessAddress.PostalCode = model.BusinessAddress.PostalCode
            .ToUpper().Replace("[^A-Z0-9]", "").Replace(" ", "");

        Console.WriteLine(model.CarBrand);

        if (!EditContext.Validate()) return;

        if (UpdateModel == null)
        {
            await ApiRequest.PostAsync<RequestResponse>(
                    ApiRoutes.CarRecords.Create, model!);
        }
        else
        {
            await ApiRequest.PutAsync<RequestResponse>(
                ApiRoutes.CarRecords.Update, model!);
        }


        await Refresh();
    }
}
