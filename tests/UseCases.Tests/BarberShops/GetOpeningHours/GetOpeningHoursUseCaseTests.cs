using BarberBoss.Exception.ExceptionsBase;
using BarberBoss.Exception;
using Common.Tests.Utilities.Entities;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;
using UseCases.Tests.UseCaseFactories.BarberShops;

namespace UseCases.Tests.BarberShops.GetOpeningHours;
public class GetOpeningHoursUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = BarberShopBuilder.Build();
        
        var dateFilter = DateTime.UtcNow;
        
        var collection = OpeningHourBuilder.Collection(loggedUser.Id, dateFilter: dateFilter);

        var useCase = GetOpeningHoursUseCaseFactory.CreateUseCase(collection, user: loggedUser, dateFiltered: DateTime.UtcNow);

        var request = RequestGetOpeningHoursJsonBuilder.Build(dateFilter);

        var result = await useCase.Execute(request);

        result.IsSuccess.Should().BeTrue();
        result.Value.OpeningHours.Count.Should().Be(2);
    }
}
