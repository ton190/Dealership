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
        : base(dbContext, mapper, validator) { }
}

public class UpdateCarRecordHandler
    : UpdateHandler<UpdateCarRecordModel, CarRecord, CarRecordDto, bool>
{
    public UpdateCarRecordHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        IValidator<CarRecordDto> validator)
        : base(dbContext, mapper, validator) { }
}


public class RemoveCarRecordHandler
    : RemoveHandler<RemoveCarRecordModel, CarRecord, bool>
{
    public RemoveCarRecordHandler(IAppDbContext dbContext) : base(dbContext) { }
}

public class GetAllCarRecordsHandler
    : GetAllHandler<GetAllCarRecordsModel, CarRecord, CarRecordDto>
{
    public GetAllCarRecordsHandler(
        IAppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

    protected override IQueryable<CarRecord> OnBefore(
        IQueryable<CarRecord> request, GetAllCarRecordsModel model)
    {
        if (string.IsNullOrEmpty(model.Search)) return request;

        return request.Where(x => x.BusinessName.Contains(
            model.Search, StringComparison.OrdinalIgnoreCase) ||
            x.FINCode.Contains(
            model.Search, StringComparison.OrdinalIgnoreCase) ||
            (x.BusinessAddress.UnitNumber
             + " - " + x.BusinessAddress.BuildingNumber
             + " " + x.BusinessAddress.StreetName
             + "," + x.BusinessAddress.City
             + "," + x.BusinessAddress.Province
             + "," + x.BusinessAddress.PostalCode).Contains(
            model.Search, StringComparison.OrdinalIgnoreCase) ||
            x.CarBrand.ToLower().Contains(
            model.Search, StringComparison.OrdinalIgnoreCase) ||
            x.ContactNames.Any(x => x.FirstName.Contains(
            model.Search, StringComparison.OrdinalIgnoreCase) ||
            x.LastName.Contains(
            model.Search, StringComparison.OrdinalIgnoreCase)) ||
            x.PhoneNumbers.Any(x => x.Number.Contains(
            model.Search, StringComparison.OrdinalIgnoreCase)));
    }
}
