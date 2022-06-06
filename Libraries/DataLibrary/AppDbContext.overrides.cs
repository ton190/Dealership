using System.Reflection;
using DataLibrary.Configurations;
using Domain.Services;
using EntityLibrary;
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
        OnBeforeSave();
        return await base.SaveChangesAsync(ct);
    }

    private void OnBeforeSave()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State != EntityState.Unchanged &&
                entry.Entity is BaseEntity trackable)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        trackable.DateCreated = Time.Now;
                        trackable.DateModified = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        trackable.DateCreated = Time.Now;
                        break;
                    case EntityState.Deleted:
                        trackable.DateModified = DateTime.Now;
                        entry.State = EntityState.Modified;
                        break;
                }
            }
        }
    }
}
