namespace ModelLibrary.Records;

public record CreateRecordModel(RecordDto Dto)
    : ICreateModel<RecordDto, bool>;

public record UpdateRecordModel(RecordDto Dto)
    : IUpdateModel<RecordDto, bool>;

public record RemoveRecordModel(int Id)
    : IRemoveModel<bool>;

public record GetAllRecordsModel(
    bool SortDescending = true, int Index = 0, int PageSize = 0,
    string? Search = null)
    : IGetAllModel<RecordDto>;

public record GetRecordsStatisticsModel()
    : IRequest<RequestResponse<RecordsStatisticsDto>>;
