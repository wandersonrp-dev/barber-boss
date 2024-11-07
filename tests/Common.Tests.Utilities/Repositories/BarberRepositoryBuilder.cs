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

    public BarberRepositoryBuilder ExistsWithSameEmail(string email, Guid id)
    {
        _repository.Setup(repository => repository.ExistsWithSameEmail(email, id)).ReturnsAsync(true);

        return this;
    }

    public IBarberRepository Build() => _repository.Object;
}
