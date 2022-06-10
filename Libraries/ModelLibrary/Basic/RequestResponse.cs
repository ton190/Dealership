using FluentValidation.Results;

namespace ModelLibrary.Basic;

public record RequestResponse(bool Success, ValidationErrors? Errors = null);

public record RequestResponse<TResponse>(
    bool Success,
    ValidationErrors? Errors = null,
    TResponse? Response = default(TResponse));

public class ValidationErrors : List<string>
{
    public ValidationErrors() { }
    public ValidationErrors(string error) => this.Add(error);
    public ValidationErrors(IEnumerable<string> errors)
        => this.AddRange(errors);
}

public static partial class Extensions
{
    public static ValidationErrors ToRequestErrors(
    this List<ValidationFailure> errors)
    {
        var result = new ValidationErrors();

        foreach (var error in errors) result.Add(error.ErrorMessage);

        return result;
    }
}
