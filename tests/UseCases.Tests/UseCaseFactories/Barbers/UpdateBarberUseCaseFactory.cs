using BarberBoss.Application.UseCases.Barbers.Update;
using BarberBoss.Application.UseCases.BarberShops.Update;
using BarberBoss.Domain.Entities;
using Common.Tests.Utilities.Cryptography;
using Common.Tests.Utilities.LoggedUser;
using Common.Tests.Utilities.Logger;
using Common.Tests.Utilities.Repositories;

namespace UseCases.Tests.UseCaseFactories.Barbers;
public class UpdateBarberUseCaseFactory
{
    public static UpdateBarberUseCase CreateUseCase(User logged, User? user = null, bool updateSuccess = true)
    {
        var barberRepository = new BarberRepositoryBuilder();
        var logger = LoggerBuilder<UpdateBarberUseCase>.Build();
        var passwordHasher = new PasswordHasherBuilder<Barber>().Build();
        var loggedUser = LoggedUserBuilder.Build(logged);

        if (user is not null)
        {
            barberRepository.GetByIdAsync((Barber)user);

            if (updateSuccess)
                barberRepository.UpdateAsync((Barber)user);
            else
                barberRepository.UpdateAsyncFailure((Barber)user);

        }

        return new UpdateBarberUseCase(barberRepository.Build(), loggedUser, logger, passwordHasher);
    }
}
