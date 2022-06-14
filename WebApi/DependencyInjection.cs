using Application;
using Application.Interfaces;
using DataLibrary;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApi.Services;

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
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<IPaymentService, PaymentService>()
            .Configure<PaymentServiceSettings>(
                builder.Configuration.GetSection("Stripe"));

        builder.Services.AddAuthentication(
            CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Events.OnRedirectToAccessDenied =
                options.Events.OnRedirectToLogin = c =>
                    {
                        c.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.FromResult<object>(new());
                    };
            });

        return builder;
    }
}
