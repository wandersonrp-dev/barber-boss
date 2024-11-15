using BarberBoss.Domain.Entities;
using BarberBoss.Infrastructure.Data;
using Common.Tests.Utilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Tests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private BarberShop _barberShop = default!;    
    private Barber _barber = default!;
    private OpeningHour _openingHour = default!; 

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

                var scope = services.BuildServiceProvider().CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<IDbContextFactory<BarberBossDbContext>>();
                var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();               

                StartDatabase(context, passwordHasher);
            });
    }
    
    public BarberShop GetBarberShop() => _barberShop;
    public Barber GetBarber() => _barber;
    public OpeningHour GetOpeningHour() => _openingHour;

    private void StartDatabase(IDbContextFactory<BarberBossDbContext> contextFactory, IPasswordHasher<User> passwordHasher)
    {
        var barberShop = BarberShopBuilder.Build();

        var passwordAux = barberShop.Password;

        _barberShop = barberShop;

        barberShop.Password = passwordHasher.HashPassword(barberShop, barberShop.Password);

        using var context = contextFactory.CreateDbContext();

        context.BarberShops.Add(barberShop);

        context.SaveChanges();
        
        var barberPaasswordAux = CreateBarber(passwordHasher);

        context.Barbers.Add(_barber);

        context.SaveChanges();

        //CreateOpeningHour(context, barberShop);

        _barber.Password = barberPaasswordAux;
        _barberShop.Password = passwordAux;
    }

    //private void CreateBarberShop(BarberBossDbContext context, IPasswordHasher<User> passwordHasher)
    //{
    //    var barberShop = BarberShopBuilder.Build();

    //    var passwordAux = barberShop.Password;

    //    _barberShop = barberShop;

    //    _barberShop.Password = passwordHasher.HashPassword(barberShop, barberShop.Password);

    //    context.BarberShops.Add(barberShop);

    //    context.SaveChanges();

    //    _barberShop.Password = passwordAux;
    //}

    private void CreateOpeningHour(BarberBossDbContext context, BarberShop barberShop)
    {        
        var openingHour = OpeningHourBuilder.Build(barberShopId: barberShop.Id, DateTime.Now);

        _openingHour = openingHour;

        context.OpeningHours.Add(openingHour);

        context.SaveChanges();
    }

    private string CreateBarber(IPasswordHasher<User> passwordHasher)
    {
        var barber = BarberBuilder.Build();

        var passwordAux = barber.Password;

        _barber = barber;

        _barber.Password = passwordHasher.HashPassword(barber, barber.Password);

        return passwordAux;
    }
}
