using EntityLibrary;

namespace Application.Abstractions;

public class GetAllHandler<TModel, TEntity, TDto>
    : IRequestHandler<TModel, RequestResponse<List<TDto>>>
    where TModel : IGetAllModel<TDto>
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    private readonly IAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllHandler(IAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<RequestResponse<List<TDto>>> Handle(
        TModel model, CancellationToken ct)
    {
        var query = _dbContext.Set<TEntity>().AsNoTracking();
        var request = OnBefore(query).ProjectTo<TDto>(_mapper.ConfigurationProvider);
        return new(true, null, await request.ToListAsync(ct));
    }

    protected virtual IQueryable<TEntity> OnBefore(
        IQueryable<TEntity> request) => request;
}
