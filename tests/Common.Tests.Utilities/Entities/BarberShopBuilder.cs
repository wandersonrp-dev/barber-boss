using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Enums;
using Bogus;
using Common.Tests.Utilities.Cryptography;

namespace Common.Tests.Utilities.Entities;
public class BarberShopBuilder
{
    public static BarberShop Build()
    {        
        return new Faker<BarberShop>()
            .RuleFor(bs => bs.Id, () => Guid.NewGuid())
            .RuleFor(bs => bs.Name, faker => faker.Company.CompanyName())
            .RuleFor(bs => bs.Email, (faker, bs) => faker.Internet.Email(firstName: bs.Name))
            .RuleFor(bs => bs.Phone, faker => faker.Phone.PhoneNumber("###########"))
            .RuleFor(bs => bs.PhoneContact, faker => faker.Person.FullName)
            .RuleFor(bs => bs.Password, (faker, bs) => new PasswordHasherBuilder<BarberShop>(bs).Build().HashPassword(bs, bs.Password))
            .RuleFor(bs => bs.UserType, () => UserType.BarberShop);            
    }
}
