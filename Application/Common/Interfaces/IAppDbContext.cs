namespace Application.Interfaces;

public interface IAppDbContext
{
    Task<int> SaveChangesAsync(CancellationToken ct);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    DbSet<Brand> Brands { get; }
    DbSet<Record> Records { get; }
    DbSet<Order> Orders { get; }
    DbSet<User> Users { get; }
}
