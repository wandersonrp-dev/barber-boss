using BarberBoss.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infrastructure;
public static class DependecyInjectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
    }

    public static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BarberBossDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("BarberBossConnection")));
    }
}
