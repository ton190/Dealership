namespace ModelLibrary.CarBrands;

public record CreateCarBrandModel(CarBrandDto Dto)
    : ICreateModel<CarBrandDto, bool>;

public record UpdateCarBrandModel(CarBrandDto Dto)
    : IUpdateModel<CarBrandDto, bool>;

public record RemoveCarBrandModel(int Id)
    : IRemoveModel<bool>;

public record GetAllCarBrandsModel(
    bool SortDescending = false, int Index = 0, int PageSize = 0)
    : IGetAllModel<CarBrandDto>;

public record GetCarRecordsStatistics()
    : IRequest<RequestResponse<CarRecordsStatisticsDto>>;
