using BarberBoss.Communication.Requests;
using Bogus;

namespace Common.Tests.Utilities.Requests;

public class RequestDateJsonBuilder
{
    public static RequestDateJson Build(DateTime? startDate = null, DateTime? endDate = null)
    {
        return new Faker<RequestDateJson>()
           .RuleFor(d => d.StartDate, faker => startDate ?? faker.Date.Future())
            .RuleFor(d => d.EndDate, (faker, d) => endDate ?? faker.Date.Future(refDate: d.StartDate))                        
            .RuleFor(d => d.Reserved, faker => faker.Random.Bool()); 
    }
}
