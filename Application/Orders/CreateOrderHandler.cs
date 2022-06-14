using System.Text.Json;
using F23.StringSimilarity;
using ModelLibrary.Orders;
using ModelLibrary.Records;

namespace Application.Orders;

public class CreateOrderHandler
    : IRequestHandler<CreateOrderModel, RequestResponse<string?>>
{
    private readonly IAppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<RecordSearchDto> _validator;
    private readonly IPaymentService _paymentService;

    public CreateOrderHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        IValidator<RecordSearchDto> validator,
        ISecretManager secretManager,
        IPaymentService paymentService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
        _paymentService = paymentService;
    }

    public async Task<RequestResponse<string?>> Handle(
        CreateOrderModel model, CancellationToken ct)
    {
        var validator = _validator.Validate(model.Dto);
        if (!validator.IsValid)
            return new(false, validator.Errors.ToRequestErrors());

        var session = await _paymentService.CreateSession(
            model.Dto.ClientEmail);
        if (session == null) return new(false, new("Stripe Error"));

        var jw = new JaroWinkler();
        var search = model.Dto;
        var dbList = await _dbContext.Records.AsNoTracking().Where(
                x => x.Brand == search.Brand)
                .ProjectTo<RecordDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

        var resultList = new ResultList();

        if (!string.IsNullOrWhiteSpace(search.BusinessName))
            resultList.AddRange(dbList.Where(x => x.BusinessName != "" &&
                jw.Similarity(x.BusinessName.ToLower(),
                    search.BusinessName.ToLower()) > 0.98));

        if (!string.IsNullOrWhiteSpace(search.ContactName.FirstName) &&
            !string.IsNullOrWhiteSpace(search.ContactName.LastName))
            resultList.AddRange(dbList.Where(x =>
                    x.ContactNames.Any(
                    x => x.FirstName == search.ContactName.FirstName &&
                    x.LastName == search.ContactName.LastName)));

        if (!string.IsNullOrWhiteSpace(search.Phone.Number))
            resultList.AddRange(dbList.Where(x => x.PhoneNumbers.Any(
                    x => x.Number == search.Phone.Number)));

        if (!string.IsNullOrWhiteSpace(search.BusinessAddress.FullAddress))
        {
            var address = search.BusinessAddress;
            resultList.AddRange(dbList
                .Where(x =>
                    x.BusinessAddress.BuildingNumber == address.BuildingNumber
                    && x.BusinessAddress.PostalCode == address.PostalCode
                    && jw.Similarity(x.BusinessAddress.StreetName.ToLower(),
                        address.StreetName.ToLower()) > 0.9));
        }

        var searchString = JsonSerializer.Serialize(model.Dto);
        var resultString = JsonSerializer.Serialize(resultList.Items);

        var item = new Order();
        item.RecordSearch = searchString;
        item.SearchResult = resultString;
        item.Email = model.Dto.ClientEmail;
        item.SessionId = session.Id;

        await _dbContext.Orders.AddAsync(item);
        var result = await _dbContext.SaveChangesAsync(ct) > 0;
        if (!result) return new(false, new("Database error"));

        return new(true, null, session.Url);
    }

    private class ResultList
    {
        public List<RecordDto> Items { get; set; } = new();
        public void AddRange(IEnumerable<RecordDto> list)
        {
            foreach (var item in list)
                if (!Items.Contains(item)) Items.Add(item);
        }
    }
}
