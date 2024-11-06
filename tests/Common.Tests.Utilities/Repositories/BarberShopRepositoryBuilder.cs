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

    public BarberShopRepositoryBuilder GetByIdAsync(BarberShop barberShop)
    {
        _repository.Setup(repository => repository.GetByIdAsync(barberShop.Id)).ReturnsAsync(barberShop);

        return this;
    }

    public BarberShopRepositoryBuilder UpdateAsync(BarberShop barberShop)
    {
        _repository.Setup(repository => repository.UpdateAsync(barberShop)).ReturnsAsync(true);

        return this;
    }

    public BarberShopRepositoryBuilder UpdateAsyncFailure(BarberShop barberShop)
    {
        _repository.Setup(repository => repository.UpdateAsync(barberShop)).ReturnsAsync(false);

        return this;
    }

    public IBarberShopRepository Build() => _repository.Object;
}
