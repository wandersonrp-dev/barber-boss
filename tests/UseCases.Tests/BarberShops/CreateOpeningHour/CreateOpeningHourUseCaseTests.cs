using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Common.Tests.Utilities.Entities;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;
using UseCases.Tests.UseCaseFactories.BarberShops;

namespace UseCases.Tests.BarberShops.CreateOpeningHour;

public class CreateOpeningHourUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = BarberShopBuilder.Build();
        var collection = RequestCreateOpeningHourJsonBuilder.Collection();

        var request = RequestCreateOpeningHourJsonBuilder.Build(collection);   

        var useCase = CreateOpeningHourUseCaseFactory.CreateUseCase(loggedUser);

        var result = await useCase.Execute(request);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Error_Invalid_Start_Date()
    {
        var loggedUser = BarberShopBuilder.Build();
        var collection = RequestCreateOpeningHourJsonBuilder.Collection(startDate: DateTime.UtcNow.AddDays(-1));

        var request = RequestCreateOpeningHourJsonBuilder.Build(collection);

        var useCase = CreateOpeningHourUseCaseFactory.CreateUseCase(loggedUser);

        var result = await useCase.Execute(request);

        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be(nameof(ErrorCodes.ErrorOnValidation));
        result.Error.Messages.Should().ContainSingle().And.Contain(error => error.Equals(ResourceErrorMessages.INVALID_START_DATE));
    }

    [Fact]
    public async Task Error_Barber_Shop_Not_Found()
    {
        var loggedUser = BarberShopBuilder.Build();
        var collection = RequestCreateOpeningHourJsonBuilder.Collection();

        var request = RequestCreateOpeningHourJsonBuilder.Build(collection);

        var useCase = CreateOpeningHourUseCaseFactory.CreateUseCase(loggedUser, barberShopExists: false);

        var result = await useCase.Execute(request);

        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be(nameof(ErrorCodes.NotFound));
        result.Error.Message.Should().Be(ResourceErrorMessages.BARBER_SHOP_NOT_FOUND);
    }
}
