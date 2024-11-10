using BarberBoss.Application.UseCases.Barbers.GetProfile;
using BarberBoss.Domain.Entities;
using Common.Tests.Utilities.LoggedUser;
using Common.Tests.Utilities.Logger;
using Common.Tests.Utilities.Repositories;

namespace UseCases.Tests.UseCaseFactories.Barbers;
public class GetBarberProfileUseCaseFactory
{
    public static GetBarberProfileUseCase CreateUseCase(User? user = null, bool barberExists = true)
    {
        var barberRepository = new BarberRepositoryBuilder();
        var logger = LoggerBuilder<GetBarberProfileUseCase>.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        if (user is not null && barberExists)
        {
            barberRepository.GetByIdAsync((Barber)user);
        }

        return new GetBarberProfileUseCase(barberRepository.Build(), logger, loggedUser);
    }
}
