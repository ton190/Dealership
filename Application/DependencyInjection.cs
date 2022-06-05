using System.Reflection;
using Application.Services;
using AutoMapper.EquivalencyExpression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModelLibrary;

namespace Application;

public static partial class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddModelLibrary();

        services.AddScoped<IDbValidator, DbValidator>();
        services.AddAutoMapper(x => x.AddCollectionMappers(),
            typeof(ModelLibrary.DependencyInjection).Assembly);

        services.AddSingleton<ISecretManager, SecretManager>()
            .Configure<SecretManagerSettings>(
                configuration.GetSection("Secrets"));

        return services;
    }
}
