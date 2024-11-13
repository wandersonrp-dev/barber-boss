using BarberBoss.Application.UseCases.BarberShops.GetOpeningHours;
using BarberBoss.Domain.Entities;
using Common.Tests.Utilities.LoggedUser;
using Common.Tests.Utilities.Logger;
using Common.Tests.Utilities.Repositories;

namespace UseCases.Tests.UseCaseFactories.BarberShops;
public class GetOpeningHoursUseCaseFactory
{
    public static GetOpeningHoursUseCase CreateUseCase(List<OpeningHour> openingHours, DateTime dateFiltered, User? user = null)
    {
        var openingHourRepository = new OpeningHourRepositoryBuilder();
        var logger = LoggerBuilder<GetOpeningHoursUseCase>.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        if (user is not null)
        {
            openingHourRepository.GetByFilteredDate(openingHours);
        }

        return new GetOpeningHoursUseCase(openingHourRepository.Build(), loggedUser, logger);
    }
}
