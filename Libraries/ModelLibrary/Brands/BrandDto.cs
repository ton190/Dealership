using EntityLibrary;
using FluentValidation;
using ModelLibrary.Records;

namespace ModelLibrary.Brands;

public class BrandDto
    : BaseDto, IMap<Brand, BrandDto>
{
    private string name =  string.Empty;
    public string Name
    {
        get => name;
        set => name = value.Trim();
    }
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
