using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using Moq;

namespace Common.Tests.Utilities.Repositories;
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

    public BarberShopRepositoryBuilder GetByEmailAsync(BarberShop barberShop)
    {
        _repository.Setup(repository => repository.GetByEmailAsync(barberShop.Email)).ReturnsAsync(barberShop);

        return this;
    }

    public IBarberShopRepository Build() => _repository.Object;
}
