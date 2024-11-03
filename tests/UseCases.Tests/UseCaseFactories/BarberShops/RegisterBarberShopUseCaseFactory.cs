using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Domain.Entities;
using Common.Tests.Utilities.Cryptography;
using Common.Tests.Utilities.Logger;
using Common.Tests.Utilities.Repositories;

namespace UseCases.Tests.UseCaseFactories.BarberShops;
public class RegisterBarberShopUseCaseFactory
{
    public static RegisterBarberShopUseCase CreateUseCase(string? email = null)
    {
        var barberShopRepository = new BarberShopRepositoryBuilder();
        var logger = LoggerBuilder<RegisterBarberShopUseCase>.Build();
        var passwordHasher = new PasswordHasherBuilder<BarberShop>().Build();

        if (!string.IsNullOrWhiteSpace(email))
        {
            barberShopRepository.ExistsWithSameEmail(email);
        }

        return new RegisterBarberShopUseCase(barberShopRepository.Build(), logger, passwordHasher);
    }
}
