using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Enums;
using Bogus;
using Common.Tests.Utilities.Cryptography;

namespace Common.Tests.Utilities.Entities;
public class BarberBuilder
{
    public static Barber Build()
    {
        return new Faker<Barber>()
            .RuleFor(bs => bs.Id, () => Guid.NewGuid())
            .RuleFor(bs => bs.Name, faker => faker.Company.CompanyName())
            .RuleFor(bs => bs.Email, (faker, bs) => faker.Internet.Email(firstName: bs.Name))
            .RuleFor(bs => bs.Phone, faker => faker.Phone.PhoneNumber("###########"))            
            .RuleFor(bs => bs.Password, (faker, bs) => new PasswordHasherBuilder<Barber>(bs).Build().HashPassword(bs, bs.Password))
            .RuleFor(bs => bs.UserType, () => UserType.BarberShop);
    }
}
