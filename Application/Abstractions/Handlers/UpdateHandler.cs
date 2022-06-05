using EntityLibrary;

namespace Application.Abstractions;

public class UpdateHandler<TModel, TEntity, TDto, TResponse>
    : IRequestHandler<TModel, RequestResponse<TResponse>>
    where TModel : IUpdateModel<TDto, TResponse>
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    private readonly IAppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<TDto> _validator;

    public UpdateHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        IValidator<TDto> validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<RequestResponse<TResponse>> Handle(
        TModel model, CancellationToken ct)
    {
        var validator = await _validator.ValidateAsync(model.Dto);
        if (!validator.IsValid)
            return new(false, validator.Errors.ToRequestErrors());

        var query = OnBefore(_dbContext.Set<TEntity>().AsQueryable());
        var item = await query.FirstOrDefaultAsync(x => x.Id == model.Dto.Id);
        if (item is null) return new(false, new("Database Error, wrong Id"));

        _mapper.Map<TDto, TEntity>(model.Dto, item);

        var result = await _dbContext.SaveChangesAsync(ct) > 0;
        if (!result) return new(false, new("Database error"));

        return new(true);
    }

    protected virtual IQueryable<TEntity> OnBefore(
        IQueryable<TEntity> query) => query;
}
