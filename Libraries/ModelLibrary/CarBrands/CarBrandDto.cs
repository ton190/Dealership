using EntityLibrary.CarRecords;
using FluentValidation;
using ModelLibrary.CarRecords;

namespace ModelLibrary.CarBrands;

public class CarBrandDto
    : BaseDto, IMap<CarBrand, CarBrandDto>
{
    public string Name { get; set; } = string.Empty;
    public virtual List<CarRecordDto> CarRecords { get; set; } = new();
}

public class CarBrandDtoValidator : AbstractValidator<CarBrandDto>
{
    public CarBrandDtoValidator(IDbValidator dbValidator)
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Name cannot be empty")
            .MustAsync(async (model, name, ct) =>
                !await dbValidator.IsCarBrandNameExists(name, model.Id, ct))
            .WithMessage("This name already exists");
    }
}
