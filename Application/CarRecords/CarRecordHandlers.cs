using EntityLibrary.CarRecords;
using ModelLibrary.CarRecords;

namespace Application.CarRecords;

public class CreateCarRecordHandler
    : CreateHandler<CreateCarRecordModel, CarRecord, CarRecordDto, bool>
{
    public CreateCarRecordHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        IValidator<CarRecordDto> validator)
        : base(dbContext, mapper, validator)
    {
    }
}

public class UpdateCarRecordHandler
    : UpdateHandler<UpdateCarRecordModel, CarRecord, CarRecordDto, bool>
{
    public UpdateCarRecordHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        IValidator<CarRecordDto> validator)
        : base(dbContext, mapper, validator)
    {
    }
}


public class RemoveCarRecordHandler
    : RemoveHandler<RemoveCarRecordModel, CarRecord, bool>
{
    public RemoveCarRecordHandler(IAppDbContext dbContext) : base(dbContext)
    {
    }
}

public class GetAllCarRecordsHandler
    : GetAllHandler<GetAllCarRecordsModel, CarRecord, CarRecordDto>
{
    public GetAllCarRecordsHandler(
        IAppDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
    }
}
