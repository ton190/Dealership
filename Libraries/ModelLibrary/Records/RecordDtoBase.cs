using FluentValidation;

namespace ModelLibrary.Records;

public abstract class RecordDtoBase : BaseDto
{
    private string businessName = string.Empty;
    public string BusinessName
    {
        get => businessName;
        set => businessName = value.Trim();
    }
    public string Brand { get; set; } = string.Empty;
    public AddressDto BusinessAddress { get; set; } = new();
}

public abstract class RecordDtoBaseValidator<TDto> : AbstractValidator<TDto>
    where TDto : RecordDtoBase
{
    public RecordDtoBaseValidator(bool searchModel = false)
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

        RuleFor(x => x.Brand)
            .NotEmpty()
            .WithMessage("Car Brand not selected");

        RuleFor(x => x.BusinessAddress)
            .SetValidator(new AddressDtoValidator(searchModel));
    }
}
