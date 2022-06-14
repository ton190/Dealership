using ModelLibrary.Brands;

namespace Application.Brands;

public class CreateBrandHandler
    : CreateHandler<CreateBrandModel, Brand, BrandDto, bool>
{
    public CreateBrandHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        IValidator<BrandDto> validator)
        : base(dbContext, mapper, validator) { }
}

public class UpdateBrandHandler
    : UpdateHandler<UpdateBrandModel, Brand, BrandDto, bool>
{
    public UpdateBrandHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        IValidator<BrandDto> validator)
        : base(dbContext, mapper, validator) { }
}

public class RemoveBrandHandler
    : RemoveHandler<RemoveBrandModel, Brand, bool>
{
    public RemoveBrandHandler(IAppDbContext dbContext) : base(dbContext) { }
}

public class GetAllBrandsHandler
    : GetAllHandler<GetAllBrandsModel, Brand, BrandDto>
{
    public GetAllBrandsHandler(
        IAppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
}
