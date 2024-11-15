using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using Moq;

namespace Common.Tests.Utilities.Repositories;

public class OpeningHourRepositoryBuilder
{
    private readonly Mock<IOpeningHourRepository> _repository;

    public OpeningHourRepositoryBuilder()
    {
        _repository = new Mock<IOpeningHourRepository>();
    }

    public void ExistsByStartAndEndDate(bool exists)
    {
        _repository.Setup(repository => repository.ExistsByStartAndEndDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Guid>())).ReturnsAsync(exists);
    }    

    public void GetByFilteredDate(List<OpeningHour> openingHours)
    {
        _repository.Setup(repository => repository.GetByFilteredDate(It.IsAny<Guid>(), It.IsAny<DateTime>())).ReturnsAsync(openingHours);
    }

    public IOpeningHourRepository Build() => _repository.Object;
}
