using EntityLibrary.CarRecords;
using ModelLibrary.CarRecords;

namespace Application.CarRecordOrders;

public class GetCarRecordOrderByTokenHandler
: IRequestHandler<GetCarRecordOrderByTokenModel,
    RequestResponse<CarRecordOrderDto>>
{
    private readonly IAppDbContext _dbContext;
    private readonly ISecretManager _secretManager;
    private readonly IMapper _mapper;

    public GetCarRecordOrderByTokenHandler(
        IAppDbContext dbContext,
        ISecretManager secretManager,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _secretManager = secretManager;
        _mapper = mapper;
    }

    public async Task<RequestResponse<CarRecordOrderDto>> Handle(
        GetCarRecordOrderByTokenModel model, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(model.Token))
            return new(false, new("Invalid Token"));

        var claims = _secretManager.ReadToken(model.Token);
        var orderClaim = claims?.FirstOrDefault(x => x.Type == "OrderId");
        if (orderClaim is null ||
            !int.TryParse(orderClaim.Value, out var orderId))
            return new(false, new("Invalid Token"));

        if (orderId == 0) return new(true, null);
        var order = await _dbContext.CarRecordOrders.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == orderId);

        if (order is null) return new(false, new("Database Error"));

        var orderDto = _mapper.Map<CarRecordOrder, CarRecordOrderDto>(order);

        return new(true, null, orderDto);
    }
}
