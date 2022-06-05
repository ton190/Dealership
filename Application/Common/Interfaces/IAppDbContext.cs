using EntityLibrary.CarRecords;

namespace Application.Interfaces;

public interface IAppDbContext
{
    Task<int> SaveChangesAsync(CancellationToken ct);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    DbSet<CarBrand> CarBrands { get; }
    DbSet<CarRecord> CarRecords { get; }
    DbSet<CarRecordOrder> CarRecordOrders { get; }
}
