using BarberBoss.Application.UseCases.BarberShops.CreateOpeningHour;
using BarberBoss.Domain.Entities;
using Common.Tests.Utilities.LoggedUser;
using Common.Tests.Utilities.Logger;
using Common.Tests.Utilities.Repositories;

namespace UseCases.Tests.UseCaseFactories.BarberShops;

public class CreateOpeningHourUseCaseFactory
{
    public static CreateOpeningHourUseCase CreateUseCase(BarberShop barberShop, bool barberShopExists = true)
    {        
        var barberShopRepository = new BarberShopRepositoryBuilder();
        var openingHourRepository = new OpeningHourRepositoryBuilder();
        var logger = LoggerBuilder<CreateOpeningHourUseCase>.Build();        
        var loggedUser = LoggedUserBuilder.Build(barberShop);

        if (barberShop is not null && barberShopExists)
        {            
            barberShopRepository.GetByIdAsync(barberShop);
        }

        return new CreateOpeningHourUseCase(barberShopRepository.Build(), openingHourRepository.Build(), logger, loggedUser);
    }
    
}
