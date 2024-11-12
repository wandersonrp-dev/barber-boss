using BarberBoss.Communication.Requests.BarberShop;
using Bogus;

namespace Common.Tests.Utilities.Requests.BarberShops;

public class RequestCreateOpeningHourJsonBuilder
{
    public static RequestCreateOpeningHourJson Build(DateTime? startDate = null, DateTime? endDate = null)
    {
        return new Faker<RequestCreateOpeningHourJson>()
            .RuleFor(oh => oh.StartDate, faker => startDate ?? faker.Date.Future())
            .RuleFor(oh => oh.EndDate, (faker, oh) => endDate ?? faker.Date.Future(refDate: oh.StartDate))                        
            .RuleFor(oh => oh.Reserved, _ => true);            
    }
}
