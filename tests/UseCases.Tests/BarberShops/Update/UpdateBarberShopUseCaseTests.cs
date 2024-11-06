using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Common.Tests.Utilities.Entities;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;
using UseCases.Tests.UseCaseFactories.BarberShops;

namespace UseCases.Tests.BarberShops.Update;
public class UpdateBarberShopUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = BarberShopBuilder.Build();
        var request = RequestBarberShopJsonBuilder.Build(loggedUser);

        var useCase = UpdateBarberShopUseCaseFactory.CreateUseCase(logged: loggedUser, user: loggedUser);
        
        var result = await useCase.Execute(request);

        result.IsSuccess.Should().BeTrue();        
    } 

    [Fact]
    public async Task Error_Barber_Shop_Not_Found()
    {
        var loggedUser = BarberShopBuilder.Build();

        var request = RequestBarberShopJsonBuilder.Build();        

        var useCase = UpdateBarberShopUseCaseFactory.CreateUseCase(logged: loggedUser);

        var result = await useCase.Execute(request);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.NotFound));
        result.Error.Message.Should().Be(ResourceErrorMessages.BARBER_SHOP_NOT_FOUND);
    }

    [Fact]
    public async Task Error_Internal_Server_Error()
    {
        var loggedUser = BarberShopBuilder.Build();

        var request = RequestBarberShopJsonBuilder.Build();

        var useCase = UpdateBarberShopUseCaseFactory.CreateUseCase(logged: loggedUser, user: loggedUser, updateSuccess: false);

        var result = await useCase.Execute(request);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.InternalServerError));        
    }
}
