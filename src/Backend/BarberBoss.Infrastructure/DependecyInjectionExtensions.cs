﻿using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Infrastructure.Data;
using BarberBoss.Infrastructure.Data.Repositories;
using BarberBoss.Infrastructure.Extensions;
using BarberBoss.Infrastructure.Security.Tokens;
using BarberBoss.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infrastructure;
public static class DependecyInjectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {                
        AddRepositories(services);
        AddToken(services, configuration);
        AddServices(services);

        if (!configuration.IsTestEnvironment())
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
        services
            .AddScoped<IBarberShopRepository, BarberShopRepository>()
            .AddScoped<IBarberRepository, BarberRepository>()
            .AddScoped<IOpeningHourRepository, OpeningHourRepository>();
    }
    
    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var signingkey = configuration.GetValue<string>("Settings:Jwt:SigningKey") ?? 
            throw new ArgumentNullException("Provide Jwt signing key");

        var expiresInMinute = configuration.GetValue<uint>("Settings:Jwt:ExpiresInMinutes");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expiresInMinute, signingkey));
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();
    }
}
