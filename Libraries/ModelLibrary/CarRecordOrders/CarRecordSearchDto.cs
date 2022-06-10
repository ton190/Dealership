using FluentValidation;
namespace ModelLibrary.CarRecords;

public class CarRecordSearchDto : CarRecordDtoBase
{
    public string ClientEmail { get; set; } = string.Empty;
    public ContactNameDto ContactName { get; set; } = new();
    public PhoneDto Phone { get; set; } = new();
}

public class CarRecordSearchDtoValidator
    : CarRecordDtoBaseValidator<CarRecordSearchDto>
{
    public CarRecordSearchDtoValidator() : base(true)
    {
        When(x => x.ClientEmail != "", () =>
        {
            RuleFor(x => x.ClientEmail)
                    .EmailAddress()
                    .WithMessage("Incorrect Email format");
        });

        RuleFor(x => x.Phone)
            .SetValidator(new PhoneNumberDtoValidator(true));
        RuleFor(x => x.ContactName)
            .SetValidator(new ContactNameDtoValidator(true));
    }
}
