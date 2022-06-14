using ModelLibrary.Brands;

namespace WebUI.Pages.Administration.Brands;

public class BrandAction : CUDAction<BrandDto>
{
    [Inject] ApiRequest ApiRequest { get; set; } = null!;
    protected override string EntityName => "Car Brand";

    protected override async Task OnRemove()
    {
        if(flag || EditContext is null) return;
        var model = (BrandDto)EditContext.Model;
        flag = true;
        await ApiRequest.DeleteAsync<RequestResponse>(
            ApiRoutes.Brands.Remove + model.Id);
        flag = false;
        await Refresh();
    }

    protected override async Task OnSubmit()
    {
        if (flag || !EditContext!.Validate()) return;

        flag = true;
        if (UpdateModel != null)
        {
            await ApiRequest.PutAsync<RequestResponse>(
                ApiRoutes.Brands.Update, EditContext.Model);
        }
        else
        {
            await ApiRequest.PostAsync<RequestResponse>(
                ApiRoutes.Brands.Create, EditContext.Model);
        }
        flag = false;
        await Refresh();
    }
}
