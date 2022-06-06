using EntityLibrary.CarRecords;
using ModelLibrary.CarRecords;

namespace Application.CarRecordOrders;

public class GetAllCarRecordOrdersHandler
    : GetAllHandler<GetAllCarRecordOrdersModel,
        CarRecordOrder, CarRecordOrderDto>
{
    public GetAllCarRecordOrdersHandler(
        IAppDbContext dbContext, IMapper mapper) : base(dbContext, mapper){}
}
