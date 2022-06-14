using ModelLibrary.Records;

namespace Application.Records;

public class CreateRecordHandler
    : CreateHandler<CreateRecordModel, Record, RecordDto, bool>
{
    public CreateRecordHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        IValidator<RecordDto> validator)
        : base(dbContext, mapper, validator) { }
}

public class UpdateRecordHandler
    : UpdateHandler<UpdateRecordModel, Record, RecordDto, bool>
{
    public UpdateRecordHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        IValidator<RecordDto> validator)
        : base(dbContext, mapper, validator) { }
}


public class RemoveRecordHandler
    : RemoveHandler<RemoveRecordModel, Record, bool>
{
    public RemoveRecordHandler(IAppDbContext dbContext) : base(dbContext) { }
}

public class GetAllRecordsHandler
    : GetAllHandler<GetAllRecordsModel, Record, RecordDto>
{
    public GetAllRecordsHandler(
        IAppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

    protected override IQueryable<Record> OnBefore(
        IQueryable<Record> request, GetAllRecordsModel model)
    {
        if (string.IsNullOrEmpty(model.Search)) return request;
        var search = model.Search.ToLower();

        request = from x in request
                  from n in x.ContactNames
                  from p in x.PhoneNumbers
                  where x.BusinessName.ToLower().Contains(search)
                  || x.FINCode.ToLower().Contains(search)
                  || x.Brand.ToLower().Contains(search)
                  || n.FirstName.ToLower().Contains(search)
                  || n.LastName.ToLower().Contains(search)
                  || p.Number.Contains(model.Search)
                  || (x.BusinessAddress.UnitNumber
                   + " - " + x.BusinessAddress.BuildingNumber
                   + " " + x.BusinessAddress.StreetName
                   + "," + x.BusinessAddress.City
                   + "," + x.BusinessAddress.Province
                   + "," + x.BusinessAddress.PostalCode).ToLower().Contains(search)
                  select x;

        return request;
    }
}
