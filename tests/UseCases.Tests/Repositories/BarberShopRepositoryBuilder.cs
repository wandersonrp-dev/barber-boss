using BarberBoss.Domain.Repositories;
using Moq;

namespace UseCases.Tests.Repositories;
public class BarberShopRepositoryBuilder
{
    private readonly Mock<IBarberShopRepository> _repository;

    public BarberShopRepositoryBuilder()
    {
        _repository = new Mock<IBarberShopRepository>();
    }    

    public void ExistsWithSameEmail(string email)
    {
        _repository.Setup(repository => repository.ExistsWithSameEmail(email)).ReturnsAsync(true);
    }

    public IBarberShopRepository Build() => _repository.Object;
}
