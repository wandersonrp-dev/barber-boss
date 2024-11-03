using BarberBoss.Application.UseCases.BarberShops.DoLogin;
using BarberBoss.Domain.Entities;
using Common.Tests.Utilities.Cryptography;
using Common.Tests.Utilities.Logger;
using Common.Tests.Utilities.Repositories;
using Common.Tests.Utilities.Tokens;

namespace UseCases.Tests.UseCaseFactories.BarberShops;
public class BarberShopDoLoginUseCaseFactory
{
    public static BarberShopDoLoginUseCase CreateUseCase(BarberShop barberShop, string password)
    {
        var barberShopRepository = new BarberShopRepositoryBuilder().GetByEmailAsync(barberShop).Build();
        var logger = LoggerBuilder<BarberShopDoLoginUseCase>.Build();
        var passwordHasher = new PasswordHasherBuilder<BarberShop>(barberShop).VerifyHashedPassword(barberShop, password).Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();

        return new BarberShopDoLoginUseCase(barberShopRepository, logger, passwordHasher, tokenGenerator);
    }
}
