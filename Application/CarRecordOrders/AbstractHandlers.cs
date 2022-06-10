using EntityLibrary.CarRecords;
using ModelLibrary.CarRecords;

namespace Application.CarRecordOrders;

public class GetAllCarRecordOrdersHandler
    : GetAllHandler<GetAllCarRecordOrdersModel, CarRecordOrder,
        CarRecordOrderDto>
{
    public GetAllCarRecordOrdersHandler(
        IAppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

    protected override IQueryable<CarRecordOrder> OnBefore(
        IQueryable<CarRecordOrder> request, GetAllCarRecordOrdersModel model)
    {
        if(model.Paid != null)
            request = request.Where(x => x.Paid == model.Paid);

        if(string.IsNullOrEmpty(model.Search)) return request;
        var id = 0;
        int.TryParse(model.Search, out id);

        return request.Where(x => x.Email.Contains(model.Search)
            || x.Id == id);
    }
}

public class UpdateCarRecordOrderHandler
    : UpdateHandler<UpdateCarRecordOrderModel, CarRecordOrder,
        CarRecordOrderDto, bool>
{
    public UpdateCarRecordOrderHandler(
        IAppDbContext dbContext,
        IMapper mapper)
            : base(dbContext, mapper){}
}

public class RemoveCarRecordOrderHandler
    : RemoveHandler<RemoveCarRecordOrderModel, CarRecordOrder, bool>
{
    public RemoveCarRecordOrderHandler(IAppDbContext dbContext)
        : base(dbContext){}
}
