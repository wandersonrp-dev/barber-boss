using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Requests.BarberShop;
using Bogus;

namespace Common.Tests.Utilities.Requests.BarberShops;
public class RequestCreateBarberJsonBuilder
{
    public static RequestCreateBarberJson Build(string? confirmPassword = null)
    {
        return new Faker<RequestCreateBarberJson>()
            .RuleFor(b => b.Name, faker => faker.Person.FullName)
            .RuleFor(b => b.Email, (faker, b) => faker.Internet.Email(firstName: b.Name))
            .RuleFor(b => b.Phone, (faker) => faker.Phone.PhoneNumber("###########"))
            .RuleFor(b => b.Password, (faker) => faker.Internet.Password(length: 20))
            .RuleFor(b => b.ConfirmPassword, (_, b) => confirmPassword is not null ? confirmPassword : b.Password)
            .RuleFor(b => b.UserStatus, () => UserStatus.Active);
    }
}