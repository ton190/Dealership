namespace ModelLibrary.Interfaces;

public interface ICreateModel<TDto, TResponse>
    : IRequest<RequestResponse<TResponse>> where TDto : BaseDto
{
    TDto Dto { get; }
}

public interface IUpdateModel<TDto, TResponse>
    : IRequest<RequestResponse<TResponse>> where TDto : BaseDto
{
    TDto Dto { get; }
}

public interface IRemoveModel<TResponse> : IRequest<RequestResponse<TResponse>>
{
    int Id { get; }
}

public interface IGetAllModel<TDto>
    : IRequest<RequestResponse<List<TDto>>> where TDto : BaseDto
{
}
