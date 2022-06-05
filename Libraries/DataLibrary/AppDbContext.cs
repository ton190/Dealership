using Application.Interfaces;
using EntityLibrary.CarRecords;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary;

public partial class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<CarBrand> CarBrands => Set<CarBrand>();
    public DbSet<CarRecord> CarRecords => Set<CarRecord>();
    public DbSet<CarRecordOrder> CarRecordOrders => Set<CarRecordOrder>();
    public DbSet<Phone> Phones => Set<Phone>();
    public DbSet<ContactName> ContactNames => Set<ContactName>();
}
