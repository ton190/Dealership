namespace ModelLibrary.Orders;

public record CreateOrderModel(
    RecordSearchDto Dto)
    : IRequest<RequestResponse<string?>>;

public record GetOrderModel(string SessionId)
    : IRequest<RequestResponse<OrderDto>>;

public record OrdersStatisticsModel()
    : IRequest<RequestResponse<OrdersStatisticsDto>>;

public record GetAllOrdersModel(
    bool SortDescending = true, int Index = 0, int PageSize = 0,
    string? Search = null)
    : IGetAllModel<OrderDto>;

public record CreateRefoundModel(string SessionId) : IRequest<RequestResponse>;
