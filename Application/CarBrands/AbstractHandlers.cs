using EntityLibrary.CarRecords;
using ModelLibrary.CarBrands;

namespace Application.CarBrands;

public class CreateCarBrandHandler
    : CreateHandler<CreateCarBrandModel, CarBrand, CarBrandDto, bool>
{
    public CreateCarBrandHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        IValidator<CarBrandDto> validator)
        : base(dbContext, mapper, validator){}
}

public class UpdateCarBrandHandler
    : UpdateHandler<UpdateCarBrandModel, CarBrand, CarBrandDto, bool>
{
    public UpdateCarBrandHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        IValidator<CarBrandDto> validator)
        : base(dbContext, mapper, validator){}

    protected override IQueryable<CarBrand> OnBefore(
        IQueryable<CarBrand> request) => request.Include(x => x.CarRecords);
}

public class RemoveCarBrandHandler
    : RemoveHandler<RemoveCarBrandModel, CarBrand, bool>
{
    public RemoveCarBrandHandler(IAppDbContext dbContext) : base(dbContext){}

    protected override IQueryable<CarBrand> OnBefore(
        IQueryable<CarBrand> request) => request.Include(x => x.CarRecords);
}

public class GetAllCarBrandsHandler
    : GetAllHandler<GetAllCarBrandsModel, CarBrand, CarBrandDto>
{
    public GetAllCarBrandsHandler(
        IAppDbContext dbContext, IMapper mapper) : base(dbContext, mapper){}
}
