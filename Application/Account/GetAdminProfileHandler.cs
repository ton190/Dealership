using ModelLibrary.Account;

namespace Application.Account;

public class GetAdminProfileHandler
    : IRequestHandler<GetAdminProfileModel, RequestResponse<UserDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetAdminProfileHandler(IAppDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<RequestResponse<UserDto>> Handle(
        GetAdminProfileModel model, CancellationToken ct)
    {
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(
            x => x.Role == "admin", ct);
        if (user is null) return new(false, new("Database error"));

        UserDto dto = new();
        dto.Id = user.Id;
        dto.Email = user.Email;

        return new(true, null, dto);
    }
}
