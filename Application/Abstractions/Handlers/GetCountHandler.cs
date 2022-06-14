namespace Application.Abstractions;

public class GetCountHandler<TModel, TEntity>
    : IRequestHandler<TModel, RequestResponse<int>>
    where TModel : IGetCountModel
    where TEntity : BaseEntity
{
    private readonly IAppDbContext _dbContext;

    public GetCountHandler(IAppDbContext dbContext) => _dbContext = dbContext;

    public async Task<RequestResponse<int>> Handle(
        TModel model, CancellationToken ct)
    {
        var result = await _dbContext.Set<TEntity>().CountAsync(ct);
        return new(true, null, result);
    }
}
