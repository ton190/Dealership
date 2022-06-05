using EntityLibrary.CarRecords;
using FluentValidation;

namespace ModelLibrary.CarRecords;

public class CarRecordDto : CarRecordDtoBase, IMap<CarRecord, CarRecordDto>
{
    public string FINCode { get; set; } = string.Empty;
    public List<PhoneDto> PhoneNumbers { get; set; } = new();
    public List<ContactNameDto> ContactNames { get; set; } = new();
}
public class CarRecordDtoValidator : CarRecordDtoBaseValidator<CarRecordDto>
{
    public CarRecordDtoValidator()
    {
        RuleFor(x => x.FINCode)
            .Must((code) =>
                code == "" ? true : RegExp.NumbersAndLetters.IsMatch(code))
            .WithMessage("Incorrect FINCode format");

            RuleForEach(x => x.PhoneNumbers)
                .Cascade(CascadeMode.Stop)
                .Must((model, item) => !model.PhoneNumbers.Any(
                    x => x.Number == item.Number && x != item))
                .WithMessage("Phone Number alredy exists")
                .SetValidator(new PhoneNumberDtoValidator());

            RuleForEach(x => x.ContactNames)
                .Cascade(CascadeMode.Stop)
                .Must((model, item) => !model.ContactNames.Any(
                    x => x.FirstName == item.FirstName &&
                    x.LastName == item.LastName && x != item))
                .WithMessage("Contact Name alredy exists")
                .SetValidator(new ContactNameDtoValidator());
    }
}
