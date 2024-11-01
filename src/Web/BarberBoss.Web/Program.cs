using BarberBoss.Web;
using BarberBoss.Web.Apis.BarberShops;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{    
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

ConfigureRefit(builder.Services, builder.Configuration);

await builder.Build().RunAsync();

static void ConfigureRefit(IServiceCollection services, IConfiguration configuration)
{
    var baseUrl = configuration.GetRequiredSection("Settings:BaseUrl").Value ?? string.Empty;

    services.AddRefitClient<IBarberShopApi>()
        .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(baseUrl));
}
