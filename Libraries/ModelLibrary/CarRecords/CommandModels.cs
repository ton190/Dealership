namespace ModelLibrary.CarRecords;

public record CreateCarRecordModel(CarRecordDto Dto)
    : ICreateModel<CarRecordDto, bool>;

public record UpdateCarRecordModel(CarRecordDto Dto)
    : IUpdateModel<CarRecordDto, bool>;

public record RemoveCarRecordModel(int Id)
    : IRemoveModel<bool>;

public record GetAllCarRecordsModel(
    bool SortDescending = true, int Index = 0, int PageSize = 0,
    string? Search = null)
    : IGetAllModel<CarRecordDto>;

public record SearchCarRecordModel(
    CarRecordSearchDto Dto, List<CarRecordDto> List)
    : IRequest<RequestResponse<string>>;

public record GetCarRecordsByTokenModel(string Token, List<CarRecordDto> List)
    : IRequest<RequestResponse<List<CarRecordDto>>>;

public record GetCarRecordsStatisticsModel()
    : IRequest<RequestResponse<CarRecordsStatisticsDto>>;
