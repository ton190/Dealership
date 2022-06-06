using EntityLibrary;

namespace Application.Abstractions;

public abstract class CreateHandler<TModel, TEntity, TDto, TResponse>
    : IRequestHandler<TModel, RequestResponse<TResponse>>
    where TModel : ICreateModel<TDto, TResponse>
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    private readonly IAppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<TDto> _validator;

    public CreateHandler(
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
        if(!validator.IsValid)
            return new(false, validator.Errors.ToRequestErrors());

        var entity = _mapper.Map<TDto, TEntity>(model.Dto);
        await _dbContext.Set<TEntity>().AddAsync(entity);
        var result = await _dbContext.SaveChangesAsync(ct) > 0;

        if (!result) return new(false, new("Database error"));
        return new(true);
    }
}
