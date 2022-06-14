using FluentValidation;

namespace ModelLibrary.Account;

public class UserDto : BaseDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator(IDbValidator dbValidator)
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Email cannot be empty")
            .EmailAddress()
            .WithMessage("Incorrect Email format")
            .MustAsync(async (model, email, ct) =>
                !await dbValidator.IsUserEmailExists(email, model.Id, ct))
            .WithMessage("This Email already exists");

        When(x => x.Password != "", () =>
        {
            RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .MinimumLength(5)
            .WithMessage("Password must be at least 5 characters long")
            .DependentRules(() =>
            {
                RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Confirm Password must be equal to Password");
            });
        });
    }
}
