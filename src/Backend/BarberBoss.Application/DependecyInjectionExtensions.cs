using BarberBoss.Application.UseCases.BarberShops.DoLogin;
using BarberBoss.Application.UseCases.BarberShops.GetProfile;
using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Application.UseCases.BarberShops.Update;
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
            .AddScoped<IBarberShopDoLoginUseCase, BarberShopDoLoginUseCase>()
            .AddScoped<IGetBarberShopProfileUseCase, GetBarberShopProfileUseCase>()
            .AddScoped<IUpdateBarberShopUseCase, UpdateBarberShopUseCase>();
    }

    private static void AddPasswordHasher(IServiceCollection services)
    {
        services.AddTransient<IPasswordHasher<BarberShop>, PasswordHasher<BarberShop>>();
    }
}
