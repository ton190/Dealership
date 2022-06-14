using ModelLibrary.Account;

namespace Application.Account;

public class UpdateAdminProfileHandler
: IRequestHandler<UpdateAdminProfileModel, RequestResponse>
{
    private readonly IAppDbContext _dbContext;
    private readonly ISecretManager _secretManager;
    private readonly IValidator<UserDto> _validator;

    public UpdateAdminProfileHandler(
        IAppDbContext dbContext,
        ISecretManager secretManager,
        IValidator<UserDto> validator)
    {
        _dbContext = dbContext;
        _secretManager = secretManager;
        _validator = validator;
    }

    public async Task<RequestResponse> Handle(
        UpdateAdminProfileModel model, CancellationToken ct)
    {
        var validator = await _validator.ValidateAsync(model.Dto);
        if(!validator.IsValid)
            return new(false, validator.Errors.ToRequestErrors());

        var user = await _dbContext.Users.Where(
            x => x.Id == model.Dto.Id).FirstOrDefaultAsync(ct);
        if(user is null)
            return new(false, new("Database error"));

        if(model.Dto.Password != "")
        {
            var hash = _secretManager.GenerateRandomString(25);
            if(hash is null) return new(false, new("Database error"));
            var password = _secretManager.HashPassword(
                model.Dto.Password, hash);
            if(password is null)
                return new(false, new("Database error"));
            user.PasswordHash = hash;
            user.Password = password;
            Console.WriteLine(hash);
            Console.WriteLine(password);
        }
        user.Email = model.Dto.Email;

        var result = await _dbContext.SaveChangesAsync(ct) > 0;
        if(!result) return new(false, new("Database error"));

        return new(true);
    }
}
