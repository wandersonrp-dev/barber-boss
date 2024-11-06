using BarberBoss.Application.UseCases.BarberShops.Update;
using BarberBoss.Domain.Entities;
using Common.Tests.Utilities.Cryptography;
using Common.Tests.Utilities.LoggedUser;
using Common.Tests.Utilities.Logger;
using Common.Tests.Utilities.Repositories;

namespace UseCases.Tests.UseCaseFactories.BarberShops;
public class UpdateBarberShopUseCaseFactory
{
    public static UpdateBarberShopUseCase CreateUseCase(User logged, User? user = null, bool updateSuccess = true)
    {
        var barberShopRepository = new BarberShopRepositoryBuilder();
        var logger = LoggerBuilder<UpdateBarberShopUseCase>.Build();
        var passwordHasher = new PasswordHasherBuilder<BarberShop>().Build();
        var loggedUser = LoggedUserBuilder.Build(logged);

        if (user is not null)
        {
            barberShopRepository.GetByIdAsync((BarberShop)user);

            if(updateSuccess)
                barberShopRepository.UpdateAsync((BarberShop)user);
            else
                barberShopRepository.UpdateAsyncFailure((BarberShop)user);

        }

        return new UpdateBarberShopUseCase(logger, barberShopRepository.Build(), loggedUser, passwordHasher);
    }
}
