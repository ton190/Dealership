namespace ModelLibrary.Brands;

public record CreateBrandModel(BrandDto Dto)
    : ICreateModel<BrandDto, bool>;

public record UpdateBrandModel(BrandDto Dto)
    : IUpdateModel<BrandDto, bool>;

public record RemoveBrandModel(int Id)
    : IRemoveModel<bool>;

public record GetAllBrandsModel(
    bool SortDescending = false, int Index = 0, int PageSize = 0)
    : IGetAllModel<BrandDto>;
