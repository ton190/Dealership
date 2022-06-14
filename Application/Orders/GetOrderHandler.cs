using ModelLibrary.Orders;

namespace Application.Orders;

public class GetOrderHandler
: IRequestHandler<GetOrderModel,
    RequestResponse<OrderDto>>
{
    private readonly IAppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IPaymentService _paymentService;

    public GetOrderHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        IPaymentService paymentService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _paymentService = paymentService;
    }

    public async Task<RequestResponse<OrderDto>> Handle(
        GetOrderModel model, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(model.SessionId))
            return new(false, new("Invalid Session Id"));

        var order = await _dbContext.Orders
            .FirstOrDefaultAsync(x => x.SessionId == model.SessionId, ct);


        if (order is null) return new(false, new("Database Error"));

        var session = await _paymentService.GetSession(order.SessionId);
        if (session == null) return new(false, new("Database Error"));

        if (order.Status != session.Status)
        {
            order.Status = session.Status;
            var result = await _dbContext.SaveChangesAsync(ct);
        }
        if (order.Status == "unpaid") return new(false, new("Not Paid"));

        var orderDto = _mapper.Map<Order, OrderDto>(order);

        return new(true, null, orderDto);
    }
}
