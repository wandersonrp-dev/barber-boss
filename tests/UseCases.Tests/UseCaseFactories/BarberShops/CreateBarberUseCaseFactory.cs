using BarberBoss.Application.UseCases.BarberShops.CreateBarber;
using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Domain.Entities;
using Common.Tests.Utilities.Cryptography;
using Common.Tests.Utilities.LoggedUser;
using Common.Tests.Utilities.Logger;
using Common.Tests.Utilities.Repositories;

namespace UseCases.Tests.UseCaseFactories.BarberShops;
public class CreateBarberUseCaseFactory
{
    public static CreateBarberUseCase CreateUseCase(BarberShop barberShop, string? email = null)
    {        
        var barberRepository = new BarberRepositoryBuilder();
        var logger = LoggerBuilder<CreateBarberUseCase>.Build();
        var passwordHasher = new PasswordHasherBuilder<Barber>().Build();
        var loggedUser = LoggedUserBuilder.Build(barberShop);

        if (!string.IsNullOrWhiteSpace(email))
        {            
            barberRepository.ExistsWithSameEmail(email, barberShop.Id);
        }

        return new CreateBarberUseCase(barberRepository.Build(), loggedUser, logger, passwordHasher);
    }
}
