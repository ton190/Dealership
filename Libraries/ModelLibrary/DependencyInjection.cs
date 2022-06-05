using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ModelLibrary;

public static partial class DependencyInjection
{
    public static IServiceCollection AddModelLibrary(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
