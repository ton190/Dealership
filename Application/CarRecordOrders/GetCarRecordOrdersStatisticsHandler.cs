using ModelLibrary.CarRecords;

namespace Application.CarRecordOrders;

public class GetCarRecordOrdersStatisticHandler
    : IRequestHandler<GetCarRecordOrdersStatisticsModel,
        RequestResponse<CarRecordOrdersStatisticsDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetCarRecordOrdersStatisticHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RequestResponse<CarRecordOrdersStatisticsDto>> Handle(
        GetCarRecordOrdersStatisticsModel mode, CancellationToken ct)
    {
        var response = new CarRecordOrdersStatisticsDto();

        response.TodayOrdersPaid = await _dbContext.CarRecordOrders.
            AsNoTracking().Where(x => x.DateCreated >= Time.Now.AddDays(-1) &&
                x.Paid).CountAsync(ct);

        response.WeekOrdersPaid = await _dbContext.CarRecordOrders.
            AsNoTracking().Where(x => x.DateCreated >= Time.Now.AddDays(-7) &&
                x.Paid).CountAsync(ct);

        response.MonthOrdersPaid = await _dbContext.CarRecordOrders.
            AsNoTracking().Where(x => x.DateCreated >= Time.Now.AddDays(-30) &&
                x.Paid).CountAsync(ct);

        response.TotalOrdersPaid = await _dbContext.CarRecordOrders.
            AsNoTracking().Where(x => x.Paid).CountAsync(ct);

        return new(true, null, response);
    }
}
