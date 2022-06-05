namespace ModelLibrary.CarRecords;

public record CreateCarRecordModel(CarRecordDto Dto)
    : ICreateModel<CarRecordDto, bool>;

public record UpdateCarRecordModel(CarRecordDto Dto)
    : IUpdateModel<CarRecordDto, bool>;

public record RemoveCarRecordModel(int Id)
    : IRemoveModel<bool>;

public record GetAllCarRecordsModel()
    : IGetAllModel<CarRecordDto>;

public record SearchCarRecordModel(
    CarRecordSearchDto Dto, List<CarRecordDto> List)
    : IRequest<RequestResponse<string>>;

public record GetCarRecordsByTokenModel(string Token, List<CarRecordDto> List)
    : IRequest<RequestResponse<List<CarRecordDto>>>;
