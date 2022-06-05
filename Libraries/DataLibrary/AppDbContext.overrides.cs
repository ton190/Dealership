using System.Reflection;
using DataLibrary.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary;

public partial class AppDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly());

        builder.SeedDefaultData();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return await base.SaveChangesAsync(ct);
    }
}
