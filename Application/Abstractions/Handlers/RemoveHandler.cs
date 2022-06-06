using EntityLibrary;

namespace Application.Abstractions;

public class RemoveHandler<TModel, TEntity, TResponse>
    : IRequestHandler<TModel, RequestResponse<TResponse>>
    where TModel : IRemoveModel<TResponse>
    where TEntity : BaseEntity
{
    private readonly IAppDbContext _dbContext;

    public RemoveHandler(IAppDbContext dbContext) => _dbContext = dbContext;

    public async Task<RequestResponse<TResponse>> Handle(
        TModel model, CancellationToken ct)
    {
        var query = OnBefore(_dbContext.Set<TEntity>().AsQueryable());
        var entity = await query.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (entity is null) return new(false, new("Database error"));

        _dbContext.Set<TEntity>().Remove(entity);
        var result = await _dbContext.SaveChangesAsync(ct) > 0;
        if (!result) return new(false, new("Database error"));

        return new(true);
    }

    protected virtual IQueryable<TEntity> OnBefore(
        IQueryable<TEntity> query) => query;
}
