using EntityLibrary;
using FluentValidation;

namespace ModelLibrary.Records;

public class ContactNameDto
    : BaseDto, IMap<ContactName, ContactNameDto>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName =>
        FirstName == "" && LastName == "" ? "" : FirstName + " " + LastName;
}

public class ContactNameDtoValidator
    : AbstractValidator<ContactNameDto>
{
    public ContactNameDtoValidator(bool searchModel = false)
    {
        RuleFor(x => x.FirstName)
            .Cascade(CascadeMode.Stop)
            .Must((name) => !(!searchModel && name == ""))
            .WithMessage("First Name cannot be empty")
            .Must((model, name) => !(model.LastName != "" && name == ""))
            .WithMessage("First Name cannot be empty");

        When(x => x.FirstName != "", () =>
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(20)
                .WithMessage("First Name mapper length must be 20 characters")
                .Matches(RegExp.Name)
                .WithMessage("Incorrect First Name format");
        });

        RuleFor(x => x.LastName)
            .Cascade(CascadeMode.Stop)
            .Must((name) => !(!searchModel && name == ""))
            .WithMessage("Last Name cannot be empty")
            .Must((model, name) => !(model.FirstName != "" && name == ""))
            .WithMessage("Last Name cannot be empty");

        When(x => x.LastName != "", () =>
        {
            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(20)
                .WithMessage("Last Name mapper length must be 20 characters")
                .Matches(RegExp.Name)
                .WithMessage("Incorrect Last Name format");
        });
    }
}
