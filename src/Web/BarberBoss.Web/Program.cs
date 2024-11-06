using BarberBoss.Web;
using BarberBoss.Web.Apis;
using BarberBoss.Web.Apis.BarberShops;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddOidcAuthentication(options =>
{    
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

builder.Services.AddAuthorizationCore();

ConfigureRefit(builder.Services, builder.Configuration);

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddTransient<AuthHeaderHandler>();

await builder.Build().RunAsync();

static void ConfigureRefit(IServiceCollection services, IConfiguration configuration)
{
    var baseUrl = configuration.GetRequiredSection("Settings:BaseUrl").Value ?? string.Empty;    

    services.AddRefitClient<IBarberShopApi>()
        .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(baseUrl))
        .AddHttpMessageHandler<AuthHeaderHandler>();
}
