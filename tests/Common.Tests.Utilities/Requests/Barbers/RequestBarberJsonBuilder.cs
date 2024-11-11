using BarberBoss.Communication.Requests.Barber;
using BarberBoss.Domain.Entities;
using Bogus;

namespace Common.Tests.Utilities.Requests.Barbers;
public class RequestBarberJsonBuilder
{
    public static RequestBarberJson Build(Barber? barber = null)
    {
        return new Faker<RequestBarberJson>()
            .RuleFor(bs => bs.Name, faker => barber is not null ? barber.Name : faker.Company.CompanyName())
            .RuleFor(bs => bs.Phone, faker => barber is not null ? barber.Phone : faker.Phone.PhoneNumber("###########"))            
            .RuleFor(bs => bs.Email, (faker, bs) => barber is not null ? barber.Email : faker.Internet.Email(firstName: bs.Name))
            .RuleFor(bs => bs.Password, faker => barber is not null ? barber.Password : faker.Internet.Password(length: 20));
    }
}
