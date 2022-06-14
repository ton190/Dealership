using Application.Interfaces;
using EntityLibrary;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary;

public partial class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Record> Records => Set<Record>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Phone> Phones => Set<Phone>();
    public DbSet<ContactName> ContactNames => Set<ContactName>();
    public DbSet<User> Users => Set<User>();
}
