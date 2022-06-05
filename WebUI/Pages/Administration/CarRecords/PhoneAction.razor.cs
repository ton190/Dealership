using System.Text.RegularExpressions;
using ModelLibrary.CarRecords;

namespace WebUI.Pages.Administration.CarRecords;

public class PhoneAction : CUDAction<PhoneDto>
{
    [Parameter] public List<PhoneDto> DtoList { get; set; } = new();
    protected override string EntityName => "Phone Number";

    protected async override Task OnRemove()
    {
        if (UpdateModel is null) return;

        DtoList.Remove(UpdateModel);
        await Refresh();
    }

    protected override async Task OnSubmit()
    {
        if (EditContext is null) return;
        var model = (PhoneDto)EditContext.Model;

        var regexObj = new Regex(@"[^\d]");
        var numberString = regexObj.Replace(model.Number, "");

        if (numberString.Length > 0)
        {
            var firstNumber = numberString.Substring(0, 1);
            if (firstNumber == "1")
                numberString = numberString.Remove(0, 1);
        }

        model.Number = numberString;
        if(!EditContext.Validate()) return;

        if (DtoList.Any(
            x => x.Number == model.Number && x != UpdateModel))
        {
            _messagesStore!.Add(EditContext.Field("PhoneNumber"),
                "Phone number already exists");
            EditContext.NotifyValidationStateChanged();
            return;
        }

        if (UpdateModel is null) DtoList.Add(model);
        else UpdateModel.Number = model.Number;

        await Refresh();
    }
}
