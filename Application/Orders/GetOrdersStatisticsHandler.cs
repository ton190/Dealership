using ModelLibrary.Orders;

namespace Application.Orders;

public class GetOrdersStatisticHandler
    : IRequestHandler<OrdersStatisticsModel,
        RequestResponse<OrdersStatisticsDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetOrdersStatisticHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RequestResponse<OrdersStatisticsDto>> Handle(
        OrdersStatisticsModel mode, CancellationToken ct)
    {
        var response = new OrdersStatisticsDto();

        response.TodayOrdersPaid = await _dbContext.Orders.
            AsNoTracking().Where(x => x.DateCreated >= Time.Now.AddDays(-1) &&
                x.Status != "unpaid").CountAsync(ct);

        response.WeekOrdersPaid = await _dbContext.Orders.
            AsNoTracking().Where(x => x.DateCreated >= Time.Now.AddDays(-7) &&
                x.Status != "unpaid").CountAsync(ct);

        response.MonthOrdersPaid = await _dbContext.Orders.
            AsNoTracking().Where(x => x.DateCreated >= Time.Now.AddDays(-30) &&
                x.Status != "unpaid").CountAsync(ct);

        response.TotalOrdersPaid = await _dbContext.Orders.
            AsNoTracking().Where(x => x.Status != "unpaid").CountAsync(ct);

        return new(true, null, response);
    }
}
