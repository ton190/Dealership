using System.Security.Claims;
using ModelLibrary.Account;

namespace Application.Account;

public class LoginHandler
    : IRequestHandler<LoginModel, RequestResponse<IEnumerable<Claim>?>>
{
    private readonly IAppDbContext _dbContext;
    private readonly ISecretManager _secretManager;
    private readonly IValidator<LoginModel> _validator;

    public LoginHandler(
        IAppDbContext dbContext,
        ISecretManager secretManager,
        IValidator<LoginModel> validator)
    {
        _dbContext = dbContext;
        _secretManager = secretManager;
        _validator = validator;
    }

    public async Task<RequestResponse<IEnumerable<Claim>?>> Handle(
        LoginModel model, CancellationToken ct)
    {
        var validator = _validator.Validate(model);
        if (!validator.IsValid)
            return new(false, validator.Errors.ToRequestErrors());

        var user = await _dbContext.Users.AsNoTracking().Where(x =>
            x.Email == model.Email.ToLower()).FirstOrDefaultAsync(ct);
        if (user is null)
            return new(false, new("Incorrect Email or Password"));

        var password = _secretManager.HashPassword(
            model.Password, user.PasswordHash);
        if (user.Password != password)
            return new(false, new("Incorrect Email or Password"));

        var claims = new Claim[]
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Role, user.Role),
        };

        return new(true, null, claims);
    }
}
