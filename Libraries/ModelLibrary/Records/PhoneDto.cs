using EntityLibrary;
using FluentValidation;

namespace ModelLibrary.Records;

public class PhoneDto
    : BaseDto, IMap<Phone, PhoneDto>
{
    private string number = string.Empty;
    public string Number
    {
        get => number;
        set => number = value.Trim();
    }
}

public class PhoneNumberDtoValidator
    : AbstractValidator<PhoneDto>
{
    public PhoneNumberDtoValidator(bool searchModel = false)
    {
        RuleFor(x => x.Number)
            .Cascade(CascadeMode.Stop)
            .Must((number) => !(!searchModel && number == ""))
            .WithMessage("Phone Number cannot be empty")
            .Must((number) =>
                number != "" ? RegExp.PhoneNumber.IsMatch(number) : true)
            .WithMessage("Incorrect Phone Number format");
    }
}
