using BarberBoss.Domain.Repositories;
using BarberBoss.Infrastructure.Data;
using BarberBoss.Infrastructure.Data.Repositories;
using BarberBoss.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infrastructure;
public static class DependecyInjectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {                
        AddRepositories(services);

        if(!configuration.IsTestEnvironment())
        {
            AddDbContext(services, configuration);
        }
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<BarberBossDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("BarberBossConnection")));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IBarberShopRepository, BarberShopRepository>();
    }    
}
