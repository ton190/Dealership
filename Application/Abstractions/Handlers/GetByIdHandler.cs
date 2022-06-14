using EntityLibrary;

namespace Application.Abstractions;

public abstract class GetByIdHandler<TModel, TEntity, TDto>
    : IRequestHandler<TModel, RequestResponse<TDto>>
    where TModel : IGetByIdModel<TDto>
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    private readonly IAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetByIdHandler(IAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<RequestResponse<TDto>> Handle(
        TModel model, CancellationToken ct)
    {
        var entity = await _dbContext.Set<TEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == model.Id, ct);
        if(entity == null) return new(true, new("No Records Found"), null);

        var dto = _mapper.Map<TEntity, TDto>(entity);
        return new(true, null, dto);
    }
}
