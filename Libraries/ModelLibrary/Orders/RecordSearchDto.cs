using FluentValidation;
using ModelLibrary.Records;

namespace ModelLibrary.Orders;

public class RecordSearchDto : RecordDtoBase
{
    public string ClientEmail { get; set; } = string.Empty;
    public ContactNameDto ContactName { get; set; } = new();
    public PhoneDto Phone { get; set; } = new();
}

public class RecordSearchDtoValidator
    : RecordDtoBaseValidator<RecordSearchDto>
{
    public RecordSearchDtoValidator() : base(true)
    {
        RuleFor(x => x.ClientEmail)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Email cannot be empty")
            .EmailAddress()
            .WithMessage("Incorrect Email format");

        RuleFor(x => x.Phone)
            .SetValidator(new PhoneNumberDtoValidator(true));
        RuleFor(x => x.ContactName)
            .SetValidator(new ContactNameDtoValidator(true));
    }
}
