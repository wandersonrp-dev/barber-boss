using BarberBoss.Exception.ExceptionsBase;
using BarberBoss.Exception;
using Common.Tests.Utilities.Entities;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;
using UseCases.Tests.UseCaseFactories.BarberShops;

namespace UseCases.Tests.BarberShops.CreateBarber;
public class CreateBarberUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = BarberShopBuilder.Build();
        var request = RequestCreateBarberJsonBuilder.Build();

        var useCase = CreateBarberUseCaseFactory.CreateUseCase(loggedUser);

        var result = await useCase.Execute(request);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Error_Required_Name()
    {
        var loggedUser = BarberShopBuilder.Build();
        var request = RequestCreateBarberJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateBarberUseCaseFactory.CreateUseCase(loggedUser);

        var result = await useCase.Execute(request);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.ErrorOnValidation));
        result.Error.Messages.Should().ContainSingle().And.Contain(error => error.Equals(ResourceErrorMessages.REQUIRED_NAME));
    }

    [Fact]
    public async Task Error_Barber_Already_Exists()
    {
        var loggedUser = BarberShopBuilder.Build();
        var request = RequestCreateBarberJsonBuilder.Build();

        var useCase = CreateBarberUseCaseFactory.CreateUseCase(loggedUser, request.Email);

        var result = await useCase.Execute(request);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.Conflict));
        result.Error.Message.Should().Be(ResourceErrorMessages.BARBER_ALREADY_EXISTS);
    }
}
