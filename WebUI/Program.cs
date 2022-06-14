using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ModelLibrary.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Logging.AddFilter("Microsoft.AspNetCore.Authorization.*", LogLevel.None);

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IDbValidator, DbValidator>();
builder.Services.AddScoped<ApiRequest>();
builder.Services.AddSingleton<AppSettings>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services
    .AddScoped<AuthenticationStateProvider, AppStateProvider>();

await builder.Build().RunAsync();
