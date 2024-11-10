using BarberBoss.Communication.Requests.Barber;
using BarberBoss.Domain.Entities;
using Bogus;

namespace Common.Tests.Utilities.Requests.Barbers;
public class RequestBarberDoLoginJsonBuilder
{
    public static RequestBarberDoLoginJson Build(Barber? barber = null)
    {
        return new Faker<RequestBarberDoLoginJson>()
            .RuleFor(bs => bs.Email, faker => barber is not null ? barber.Email : faker.Internet.Email())
            .RuleFor(bs => bs.Password, faker => faker.Internet.Password());
    }
}
