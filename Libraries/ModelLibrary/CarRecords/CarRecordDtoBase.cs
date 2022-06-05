using FluentValidation;

namespace ModelLibrary.CarRecords;

public abstract class CarRecordDtoBase : BaseDto
{
    public string BusinessName { get; set; } = string.Empty;
    public string CarBrand { get; set; } = string.Empty;
    public AddressDto BusinessAddress { get; set; } = new();
}

public abstract class CarRecordDtoBaseValidator<TDto> : AbstractValidator<TDto>
    where TDto : CarRecordDtoBase
{
    public CarRecordDtoBaseValidator(bool searchModel = false)
    {
        RuleFor(x => x.BusinessName)
            .Must((name) => !(!searchModel && name == ""))
            .WithMessage("Business Name cannot be empty");
        When(x => x.BusinessName != "", () =>
        {
            RuleFor(x => x.BusinessName)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(50)
                .WithMessage(
                    "Business Name maximum length must be 50 characters")
                .Must((name) => RegExp.BusinessName.IsMatch(name))
                .WithMessage("Incorrect Business Name format");
        });


        RuleFor(x => x.CarBrand)
            .NotEmpty()
            .WithMessage("Car Brand not selected");


        RuleFor(x => x.BusinessAddress)
            .SetValidator(new AddressDtoValidator(searchModel));
    }
}
