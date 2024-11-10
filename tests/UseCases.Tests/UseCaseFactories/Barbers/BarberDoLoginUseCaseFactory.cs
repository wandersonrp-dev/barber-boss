using BarberBoss.Application.UseCases.Barbers.DoLogin;
using BarberBoss.Domain.Entities;
using Common.Tests.Utilities.Cryptography;
using Common.Tests.Utilities.Logger;
using Common.Tests.Utilities.Repositories;
using Common.Tests.Utilities.Tokens;

namespace UseCases.Tests.UseCaseFactories.Barbers;
public class BarberDoLoginUseCaseFactory
{
    public static BarberDoLoginUseCase CreateUseCase(Barber barber, string password)
    {
        var barberShopRepository = new BarberRepositoryBuilder().GetByEmailAsync(barber).Build();
        var logger = LoggerBuilder<BarberDoLoginUseCase>.Build();
        var passwordHasher = new PasswordHasherBuilder<Barber>(barber).VerifyHashedPassword(barber, password).Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();

        return new BarberDoLoginUseCase(barberShopRepository, logger, passwordHasher, tokenGenerator);
    }
}
