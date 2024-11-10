using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using Moq;

namespace Common.Tests.Utilities.Repositories;
public class BarberRepositoryBuilder
{
    private readonly Mock<IBarberRepository> _repository;

    public BarberRepositoryBuilder()
    {
        _repository = new Mock<IBarberRepository>();
    }

    public BarberRepositoryBuilder ExistsWithSameEmail(string email)
    {
        _repository.Setup(repository => repository.ExistsWithSameEmail(email)).ReturnsAsync(true);

        return this;
    }

    public BarberRepositoryBuilder GetByEmailAsync(Barber barber)
    {
        _repository.Setup(repository => repository.GetByEmailAsync(barber.Email)).ReturnsAsync(barber);

        return this;
    }

    public IBarberRepository Build() => _repository.Object;
}
