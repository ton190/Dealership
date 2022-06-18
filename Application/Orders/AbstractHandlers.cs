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
        if (string.IsNullOrEmpty(model.Search))
        {
            request = from x in request
                      where x.Status != "unpaid"
                      select new Order
                      {
                          Id = x.Id,
                          Status = x.Status,
                          Email = x.Email,
                          DateCreated = x.DateCreated,
                          DateModified = x.DateModified
                      };
            return request;
        }

        var search = model.Search.ToLower().Trim();
        var id = 0;
        int.TryParse(search, out id);

        request = from x in request
                  where x.Id == id
                  || x.Email.Contains(search)
                  || x.Status.Contains(search)
                  select new Order
                  {
                      Id = x.Id,
                      Status = x.Status,
                      Email = x.Email,
                      DateCreated = x.DateCreated,
                      DateModified = x.DateModified
                  };

        return request.Distinct();
    }
}
