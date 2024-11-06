using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Domain.Entities;
using Bogus;

namespace Common.Tests.Utilities.Requests.BarberShops;
public class RequestBarberShopJsonBuilder
{
    public static RequestBarberShopJson Build(BarberShop? barberShop = null)
    {
        return new Faker<RequestBarberShopJson>()
            .RuleFor(bs => bs.Name, faker => barberShop is not null ? barberShop.Name : faker.Company.CompanyName())
            .RuleFor(bs => bs.Phone, faker => barberShop is not null ? barberShop.Phone : faker.Phone.PhoneNumber("###########"))
            .RuleFor(bs => bs.PhoneContact, faker => barberShop is not null ? barberShop.PhoneContact : faker.Person.FullName)
            .RuleFor(bs => bs.Email, (faker, bs) => barberShop is not null ? barberShop.Email : faker.Internet.Email(firstName: bs.Name))
            .RuleFor(bs => bs.Password, faker => barberShop is not null ? barberShop.Password : faker.Internet.Password(length: 20));            
    }
}
