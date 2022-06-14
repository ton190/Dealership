namespace Application;

public class DbValidator : IDbValidator
{
    private readonly IAppDbContext _dbContext;

    public DbValidator(IAppDbContext dbContext) => _dbContext = dbContext;

    public async Task<bool> IsBrandNameExists(
        string name, int id, CancellationToken ct)
        => await _dbContext.Brands.AsNoTracking().AnyAsync(
            x => x.Name.ToLower() == name.Trim().ToLower() && x.Id != id, ct);

    public async Task<bool> IsUserEmailExists(
        string email, int id, CancellationToken ct)
        => await _dbContext.Users.AsNoTracking().AnyAsync(
            x => x.Email.ToLower() == email.Trim().ToLower() 
                && x.Id != id, ct);
}
