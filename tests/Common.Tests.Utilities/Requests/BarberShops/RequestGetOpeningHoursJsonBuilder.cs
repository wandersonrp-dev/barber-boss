using BarberBoss.Communication.Requests.BarberShop;
using Bogus;

namespace Common.Tests.Utilities.Requests.BarberShops;
public class RequestGetOpeningHoursJsonBuilder
{
    public static RequestGetOpeningHoursJson Build(DateTime? dateFilter = null)
    {
        return new Faker<RequestGetOpeningHoursJson>()
            .RuleFor(oh => oh.DateFilter, faker => dateFilter is not null ? dateFilter : faker.Date.Future());
    }
}
