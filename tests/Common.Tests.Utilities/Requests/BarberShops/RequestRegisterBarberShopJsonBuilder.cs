using BarberBoss.Communication.Requests.BarberShop;
using Bogus;

namespace Common.Tests.Utilities.Requests.BarberShops;
public static class RequestRegisterBarberShopJsonBuilder
{
    public static RequestRegisterBarberShopJson Build()
    {
        return new Faker<RequestRegisterBarberShopJson>()
            .RuleFor(bs => bs.Name, faker => faker.Company.CompanyName())
            .RuleFor(bs => bs.Phone, faker => faker.Phone.PhoneNumber("###########"))
            .RuleFor(bs => bs.PhoneContact, faker => faker.Person.FullName)
            .RuleFor(bs => bs.Email, (faker, bs) => faker.Internet.Email(firstName: bs.Name))
            .RuleFor(bs => bs.Password, faker => faker.Internet.Password(length: 20))
            .RuleFor(bs => bs.ConfirmPassword, (faker, bs) => bs.Password);            
    }                        
}
