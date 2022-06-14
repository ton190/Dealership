using ModelLibrary.Records;

namespace Application.Records;

public class GetRecordsStatisticHandler
    : IRequestHandler<GetRecordsStatisticsModel,
        RequestResponse<RecordsStatisticsDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetRecordsStatisticHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RequestResponse<RecordsStatisticsDto>> Handle(
        GetRecordsStatisticsModel mode, CancellationToken ct)
    {
        var response = new RecordsStatisticsDto();

        response.TodayNewRecords = await _dbContext.Records.
            AsNoTracking().Where(x => x.DateCreated >= Time.Now.AddDays(-1))
            .CountAsync(ct);

        response.WeekNewRecords = await _dbContext.Records.
            AsNoTracking().Where(x => x.DateCreated >= Time.Now.AddDays(-7))
            .CountAsync(ct);

        response.MonthNewRecords = await _dbContext.Records.
            AsNoTracking().Where(x => x.DateCreated >= Time.Now.AddDays(-30))
            .CountAsync(ct);

        response.TotalNewRecords = await _dbContext.Records.
            AsNoTracking().CountAsync(ct);

        return new(true, null, response);
    }
}
