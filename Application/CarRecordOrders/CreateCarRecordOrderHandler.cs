using System.Security.Claims;
using System.Text.Json;
using EntityLibrary.CarRecords;
using F23.StringSimilarity;
using ModelLibrary.CarRecords;

namespace Application.CarRecords;

public class CreateCarRecordOrderHandler
    : IRequestHandler<CreateCarRecordOrderModel, RequestResponse<string>>
{
    private readonly IAppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<CarRecordSearchDto> _validator;
    private readonly ISecretManager _secretManager;

    public CreateCarRecordOrderHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        IValidator<CarRecordSearchDto> validator,
        ISecretManager secretManager)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
        _secretManager = secretManager;
    }

    public async Task<RequestResponse<string>> Handle(
        CreateCarRecordOrderModel model, CancellationToken ct)
    {
        var validator = _validator.Validate(model.Dto);
        if (!validator.IsValid)
            return new(false, validator.Errors.ToRequestErrors());

        var jw = new JaroWinkler();
        var search = model.Dto;
        var dbList = await _dbContext.CarRecords.AsNoTracking().Where(
                x => x.CarBrand == search.CarBrand)
                .ProjectTo<CarRecordDto>(_mapper.ConfigurationProvider)
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

        var item = new CarRecordOrder();
        item.RecordSearch = searchString;
        item.SearchResult = resultString;
        item.Email = model.Dto.ClientEmail;

        await _dbContext.CarRecordOrders.AddAsync(item);
        var result = await _dbContext.SaveChangesAsync(ct) > 0;
        if (!result) return new(false, new("Database error"));

        var token = _secretManager.GenerateToken(new Claim[]{
            new Claim("OrderId", item.Id.ToString())}, Time.Now.AddDays(30));
        return new(true, null, token);
    }

    private class ResultList
    {
        public List<CarRecordDto> Items { get; set; } = new();
        public void AddRange(IEnumerable<CarRecordDto> list)
        {
            foreach (var item in list)
                if (!Items.Contains(item)) Items.Add(item);
        }
    }
}
