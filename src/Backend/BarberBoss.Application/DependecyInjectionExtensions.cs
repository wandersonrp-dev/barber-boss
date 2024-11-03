using BarberBoss.Application.UseCases.BarberShops.DoLogin;
using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Application;
public static class DependecyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddPasswordHasher(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services
            .AddScoped<IRegisterBarberShopUseCase, RegisterBarberShopUseCase>()
            .AddScoped<IBarberShopDoLoginUseCase, BarberShopDoLoginUseCase>();
    }

    private static void AddPasswordHasher(IServiceCollection services)
    {
        services.AddTransient<IPasswordHasher<BarberShop>, PasswordHasher<BarberShop>>();
    }
}
