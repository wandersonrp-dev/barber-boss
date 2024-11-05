using BarberBoss.Application.UseCases.BarberShops.GetProfile;
using BarberBoss.Domain.Entities;
using Common.Tests.Utilities.LoggedUser;
using Common.Tests.Utilities.Logger;
using Common.Tests.Utilities.Repositories;

namespace UseCases.Tests.UseCaseFactories.BarberShops;
public class GetBarberShopProfileUseCaseFactory
{
    public static GetBarberShopProfileUseCase CreateUseCase(User? user = null)
    {
        var barberShopRepository = new BarberShopRepositoryBuilder();
        var logger = LoggerBuilder<GetBarberShopProfileUseCase>.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        if(user is not null)
        {
            barberShopRepository.GetByIdAsync((BarberShop)user);
        }

        return new GetBarberShopProfileUseCase(logger, barberShopRepository.Build(), loggedUser);
    }
}
