using ModelLibrary.Interfaces;

namespace Application;

public class DbValidator : IDbValidator
{
    private readonly IAppDbContext _dbContext;

    public DbValidator(IAppDbContext dbContext) => _dbContext = dbContext;

    public async Task<bool> IsCarBrandNameExists(
        string name, int id, CancellationToken ct)
        => await _dbContext.CarBrands.AsNoTracking().AnyAsync(
            x => x.Name.ToLower() == name.Trim().ToLower() && x.Id != id, ct);
}
