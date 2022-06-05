using ModelLibrary.CarBrands;

namespace WebUI.Pages.Administration.CarBrands;

public class BrandAction : CUDAction<CarBrandDto>
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected override string EntityName => "Car Brand";

    protected override async Task OnRemove()
    {
        if(EditContext is null) return;
        var model = (CarBrandDto)EditContext.Model;
        await ApiRequest.DeleteAsync<RequestResponse>(
            ApiRoutes.CarBrands.Remove + model.Id);
        await Refresh();
    }

    protected override async Task OnSubmit()
    {
        if (!EditContext!.Validate()) return;

        if (UpdateModel != null)
        {
            await ApiRequest.PutAsync<RequestResponse>(
                ApiRoutes.CarBrands.Update, EditContext.Model);
        }
        else
        {
            await ApiRequest.PostAsync<RequestResponse>(
                ApiRoutes.CarBrands.Create, EditContext.Model);
        }
        await Refresh();
    }
}
