using BarberBoss.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Tests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {        
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContextFactory<BarberBossDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });
            });
    }
}
