namespace ModelLibrary.Account;

public record UpdateAdminProfileModel(UserDto Dto)
    : IRequest<RequestResponse>;

public record GetAdminProfileModel()
    : IRequest<RequestResponse<UserDto>>;
