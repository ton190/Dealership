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
    : IRequest<RequestResponse<ListQuery<TDto>>> where TDto : BaseDto
{
    bool SortDescending { get; }
    int Index { get; }
    int PageSize { get; }
}

public interface IGetByIdModel<TDto>
    : IRequest<RequestResponse<TDto>> where TDto : BaseDto
{
    int Id { get; }
}

public interface IGetCountModel : IRequest<RequestResponse<int>> { }
