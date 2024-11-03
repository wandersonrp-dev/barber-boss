using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Domain.Entities;
using Bogus;

namespace Common.Tests.Utilities.Requests.BarberShops;
public class RequestBarberShopDoLoginJsonBuilder
{
    public static RequestBarberShopDoLoginJson Build(BarberShop? barberShop = null)
    {       
        return new Faker<RequestBarberShopDoLoginJson>()
            .RuleFor(bs => bs.Email, faker => barberShop is not null ? barberShop.Email : faker.Internet.Email())
            .RuleFor(bs => bs.Password, faker => faker.Internet.Password());
    }
}
