using EntityLibrary.CarRecords;
using FluentValidation;

namespace ModelLibrary.CarRecords;

public class PhoneDto
    : BaseDto, IMap<Phone, PhoneDto>
{
    public string Number { get; set; } = string.Empty;
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
