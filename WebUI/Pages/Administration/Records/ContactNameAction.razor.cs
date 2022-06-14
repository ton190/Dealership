using ModelLibrary.Records;

namespace WebUI.Pages.Administration.Records;

public class ContactNameAction : CUDAction<ContactNameDto>
{
    [Parameter] public List<ContactNameDto> DtoList { get; set; } = new();
    protected override string EntityName => "Contact Name";

    protected override void OnInitialized() => scrollAction = false;

    protected override async Task OnRemove()
    {
        if (UpdateModel is null) return;
        DtoList.Remove(UpdateModel);
        await Refresh();
    }

    protected override async Task OnSubmit()
    {
        if (EditContext is null) return;
        var model = (ContactNameDto)EditContext.Model;

        if (!EditContext.Validate()) return;

        if (DtoList.Any(
            x => x.FirstName == model.FirstName &&
            x.LastName == model.LastName && x != UpdateModel))
        {
            _messagesStore!.Add(EditContext.Field("ContactName"),
                "Contact name already exists");
            EditContext.NotifyValidationStateChanged();
            return;
        }

        if (UpdateModel is null)
        {
            DtoList.Add(model);
        }
        else
        {
            UpdateModel.FirstName = model.FirstName;
            UpdateModel.LastName = model.LastName;
        }
        await Refresh();
    }
}
