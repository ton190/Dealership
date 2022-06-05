namespace ModelLibrary.CarRecords;

public record CreateCarRecordOrderModel(
    CarRecordSearchDto Dto, List<CarRecordDto> DbList)
    : IRequest<RequestResponse<string>>;

public record GetCarRecordOrderByTokenModel(string Token)
    : IRequest<RequestResponse<CarRecordOrderDto>>;
