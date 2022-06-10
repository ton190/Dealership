using ModelLibrary.CarRecords;

namespace Application.CarRecordOrders;

public class GetCarRecordsStatisticHandler
    : IRequestHandler<GetCarRecordsStatisticsModel,
        RequestResponse<CarRecordsStatisticsDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetCarRecordsStatisticHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RequestResponse<CarRecordsStatisticsDto>> Handle(
        GetCarRecordsStatisticsModel mode, CancellationToken ct)
    {
        var response = new CarRecordsStatisticsDto();

        response.TodayNewRecords = await _dbContext.CarRecords.
            AsNoTracking().Where(x => x.DateCreated >= Time.Now.AddDays(-1))
            .CountAsync(ct);

        response.WeekNewRecords = await _dbContext.CarRecords.
            AsNoTracking().Where(x => x.DateCreated >= Time.Now.AddDays(-7))
            .CountAsync(ct);

        response.MonthNewRecords = await _dbContext.CarRecords.
            AsNoTracking().Where(x => x.DateCreated >= Time.Now.AddDays(-30))
            .CountAsync(ct);

        response.TotalNewRecords = await _dbContext.CarRecords.
            AsNoTracking().CountAsync(ct);

        return new(true, null, response);
    }
}
