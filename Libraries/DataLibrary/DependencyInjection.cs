using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataLibrary;

public static partial class DependencyInjection
{
    public static IServiceCollection AddDataLibrary(
        this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("Dealership");
                options.EnableSensitiveDataLogging();
            });
        }
        else
        {
            services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(
                     configuration.GetConnectionString("DefaultConnection"),
                     o => o.UseQuerySplittingBehavior(
                        QuerySplittingBehavior.SplitQuery)));
        }

        services.AddScoped<IAppDbContext, AppDbContext>();

        return services;
    }
}
