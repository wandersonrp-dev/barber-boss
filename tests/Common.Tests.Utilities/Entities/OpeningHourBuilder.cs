using BarberBoss.Domain.Entities;
using Bogus;

namespace Common.Tests.Utilities.Entities;
public class OpeningHourBuilder
{
    public static List<OpeningHour> Collection(Guid barberShopId, uint length = 2, DateTime? dateFilter = null)
    {
        List<OpeningHour> openingHours = new();

        if (length == 0)
            length = 1;

        for(var i = 0; i < length; i++)
        {
            var faker = new Faker<OpeningHour>()
                .RuleFor(oh => oh.StartDate, faker => dateFilter is not null ? dateFilter : faker.Date.Future())
                .RuleFor(oh => oh.EndDate, (faker, oh) => faker.Date.Future(refDate: oh.StartDate))
                .RuleFor(oh => oh.Reserved, faker => faker.Random.Bool())
                .RuleFor(oh => oh.BarberShopId, _ => barberShopId);

            openingHours.Add(faker);
        }

        return openingHours;
    }

    public static OpeningHour Build(Guid barberShopId, DateTime? startDate = null)
    {
        return new Faker<OpeningHour>()
            .RuleFor(oh => oh.StartDate, faker => startDate is not null ? startDate : faker.Date.Future())
            .RuleFor(oh => oh.EndDate, (faker, oh) => faker.Date.Future(refDate: oh.StartDate))
            .RuleFor(oh => oh.Reserved, faker => faker.Random.Bool())
            .RuleFor(oh => oh.BarberShopId, _ => barberShopId);
    }
}
