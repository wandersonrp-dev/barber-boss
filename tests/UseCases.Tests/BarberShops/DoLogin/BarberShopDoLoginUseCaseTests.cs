using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Common.Tests.Utilities.Entities;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;
using UseCases.Tests.UseCaseFactories.BarberShops;

namespace UseCases.Tests.BarberShops.DoLogin;
public class BarberShopDoLoginUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var barberShop = BarberShopBuilder.Build();

        var request = RequestBarberShopDoLoginJsonBuilder.Build(barberShop);        

        var useCase = BarberShopDoLoginUseCaseFactory.CreateUseCase(barberShop, barberShop.Password);        

        var result = await useCase.Execute(request);

        result.IsSuccess.Should().BeTrue();
        result.Value.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Email_Invalid_Credentials()
    {
        var barberShop = BarberShopBuilder.Build();

        var request = RequestBarberShopDoLoginJsonBuilder.Build();

        var useCase = BarberShopDoLoginUseCaseFactory.CreateUseCase(barberShop, barberShop.Password);

        var result = await useCase.Execute(request);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.InvalidCredential));
        result.Error.Message.Should().Be(ResourceErrorMessages.INVALID_CREDENTIALS);
    }

    [Fact]
    public async Task Error_Password_Match_Invalid_Credentials()
    {
        var barberShop = BarberShopBuilder.Build();

        var request = RequestBarberShopDoLoginJsonBuilder.Build(barberShop);

        var useCase = BarberShopDoLoginUseCaseFactory.CreateUseCase(barberShop, request.Password);

        var result = await useCase.Execute(request);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.InvalidCredential));
        result.Error.Message.Should().Be(ResourceErrorMessages.INVALID_CREDENTIALS);
    }
}
