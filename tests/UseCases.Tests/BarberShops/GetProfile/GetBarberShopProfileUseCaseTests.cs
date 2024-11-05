using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Common.Tests.Utilities.Entities;
using FluentAssertions;
using UseCases.Tests.UseCaseFactories.BarberShops;

namespace UseCases.Tests.BarberShops.GetProfile;
public class GetBarberShopProfileUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = BarberShopBuilder.Build();
        var useCase = GetBarberShopProfileUseCaseFactory.CreateUseCase(loggedUser);

        var result = await useCase.Execute();

        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(loggedUser.Id);
    }

    [Fact]
    public async Task Error_Logged_User_Not_Found()
    {
        var useCase = GetBarberShopProfileUseCaseFactory.CreateUseCase();

        var result = await useCase.Execute();

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.NotFound));
        result.Error.Message.Should().Be(ResourceErrorMessages.BARBER_SHOP_NOT_FOUND);
    }
}
