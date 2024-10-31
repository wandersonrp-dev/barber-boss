using BarberBoss.Application.UseCases.BarberShop.Register;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Application;
public static class DependecyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
    }

    public static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterBarberShopUseCase, RegisterBarberShopUseCase>();
    }
}
