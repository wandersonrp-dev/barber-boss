using BarberBoss.Exception.ExceptionsBase;
using BarberBoss.Exception;
using Common.Tests.Utilities.Entities;
using Common.Tests.Utilities.Requests.BarberShops;
using UseCases.Tests.UseCaseFactories.BarberShops;
using FluentAssertions;
using Common.Tests.Utilities.Requests.Barbers;
using UseCases.Tests.UseCaseFactories.Barbers;

namespace UseCases.Tests.Barbers.Update;
public class UpdateBarberUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = BarberBuilder.Build();
        var request = RequestBarberJsonBuilder.Build(loggedUser);

        var useCase = UpdateBarberUseCaseFactory.CreateUseCase(logged: loggedUser, user: loggedUser);

        var result = await useCase.Execute(request);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Error_Barber_Shop_Not_Found()
    {
        var loggedUser = BarberBuilder.Build();

        var request = RequestBarberJsonBuilder.Build();

        var useCase = UpdateBarberUseCaseFactory.CreateUseCase(logged: loggedUser);

        var result = await useCase.Execute(request);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.NotFound));
        result.Error.Message.Should().Be(ResourceErrorMessages.BARBER_NOT_FOUND);
    }

    [Fact]
    public async Task Error_Internal_Server_Error()
    {
        var loggedUser = BarberBuilder.Build();

        var request = RequestBarberJsonBuilder.Build();

        var useCase = UpdateBarberUseCaseFactory.CreateUseCase(logged: loggedUser, user: loggedUser, updateSuccess: false);

        var result = await useCase.Execute(request);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.InternalServerError));
    }
}
