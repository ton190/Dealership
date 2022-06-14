using EntityLibrary;
using FluentValidation;
using ModelLibrary.Records;

namespace ModelLibrary.Brands;

public class BrandDto
    : BaseDto, IMap<Brand, BrandDto>
{
    public string Name { get; set; } = string.Empty;
    public virtual List<RecordDto> Records { get; set; } = new();
}

public class BrandDtoValidator : AbstractValidator<BrandDto>
{
    public BrandDtoValidator(IDbValidator dbValidator)
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Name cannot be empty")
            .MustAsync(async (model, name, ct) =>
                !await dbValidator.IsBrandNameExists(name, model.Id, ct))
            .WithMessage("This name already exists");
    }
}
