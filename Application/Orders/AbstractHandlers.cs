using ModelLibrary.Orders;

namespace Application.Orders;

public class GetAllOrdersHandler
    : GetAllHandler<GetAllOrdersModel, Order,
        OrderDto>
{
    public GetAllOrdersHandler(
        IAppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

    protected override IQueryable<Order> OnBefore(
        IQueryable<Order> request, GetAllOrdersModel model)
    {
            request = from x in request
                      where x.Status != "unpaid"
                      select x;

        if (string.IsNullOrEmpty(model.Search)) return request;
        var id = 0;
        int.TryParse(model.Search, out id);

        request = from x in request
                  where x.Id == id
                  || x.Email.ToLower().Contains(model.Search.ToLower())
                  || x.Status.Contains(model.Search.ToLower())
                  select x;

        return request;
    }
}
