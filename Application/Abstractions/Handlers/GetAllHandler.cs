using Application.Extensions;
using EntityLibrary;

namespace Application.Abstractions;

public class GetAllHandler<TModel, TEntity, TDto>
    : IRequestHandler<TModel, RequestResponse<ListQuery<TDto>>>
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

    public async Task<RequestResponse<ListQuery<TDto>>> Handle(
        TModel model, CancellationToken ct)
    {
        var query = _dbContext.Set<TEntity>().AsNoTracking();
        var request = OnBefore(query, model).OrderBy(x => x.Id)
            .ProjectTo<TDto>(_mapper.ConfigurationProvider);
        if(model.SortDescending)
            request = request.OrderByDescending(x => x.Id);

        var items = await request.ToQueryListAsync(
            model.Index, model.PageSize, ct);

        return new(true, null, items);
    }

    protected virtual IQueryable<TEntity> OnBefore(
        IQueryable<TEntity> request, TModel model) => request;
}
