using ModelLibrary.Brands;
using ModelLibrary.Records;

namespace WebUI.Pages.Administration.Records;

public class RecordActionBase : CUDAction<RecordDto>
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    [Parameter] public List<BrandDto> Brands { get; set; } = new();

    protected override string EntityName => "Car Record";
    protected PhoneAction PhoneAction { get; set; } = null!;
    protected ContactNameAction ContactNameAction { get; set; } = null!;

    protected override async Task OnRemove()
    {
        if (UpdateModel is null) return;

        await ApiRequest.DeleteAsync<RequestResponse<bool>>(
            ApiRoutes.Records.Remove + UpdateModel!.Id);
        await Refresh();
    }

    protected override async Task OnSubmit()
    {
        if (flag || EditContext is null) return;
        var model = (RecordDto)EditContext.Model;

        model.BusinessAddress.PostalCode = model.BusinessAddress.PostalCode
            .ToUpper().Replace("[^A-Z0-9]", "").Replace(" ", "");

        if (!EditContext.Validate()) return;

        flag = true;
        if (UpdateModel == null)
        {
            await ApiRequest.PostAsync<RequestResponse>(
                    ApiRoutes.Records.Create, model!);
        }
        else
        {
            await ApiRequest.PutAsync<RequestResponse>(
                ApiRoutes.Records.Update, model!);
        }
        flag = false;

        await Refresh();
    }
}
