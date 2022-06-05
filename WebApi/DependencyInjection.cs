using Application;
using DataLibrary;

public static partial class DependencyInjection
{
    public static WebApplicationBuilder RegisterServices(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddDataLibrary(builder.Configuration);
        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddRazorPages();
        builder.Services.AddMemoryCache();
        return builder;
    }
}
