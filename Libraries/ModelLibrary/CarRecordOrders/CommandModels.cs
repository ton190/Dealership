namespace ModelLibrary.CarRecords;

public record CreateCarRecordOrderModel(CarRecordSearchDto Dto)
    : IRequest<RequestResponse<string>>;

public record UpdateCarRecordOrderModel(CarRecordOrderDto Dto)
    : IUpdateModel<CarRecordOrderDto, bool>;

public record RemoveCarRecordOrderModel(int Id)
    : IRemoveModel<bool>;

public record GetCarRecordOrderByTokenModel(string Token)
    : IRequest<RequestResponse<CarRecordOrderDto>>;

public record GetCarRecordOrdersStatisticsModel()
    : IRequest<RequestResponse<CarRecordOrdersStatisticsDto>>;

public record GetCarRecordOrdersCountModel() : IGetCountModel;

public record GetAllCarRecordOrdersModel(
    bool SortDescending = true, int Index = 1, int PageSize = 0,
    string? Search = null, bool? Paid = null)
    : IGetAllModel<CarRecordOrderDto>;
