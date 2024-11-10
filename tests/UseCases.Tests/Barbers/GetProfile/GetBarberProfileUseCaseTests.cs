using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Common.Tests.Utilities.Entities;
using FluentAssertions;
using UseCases.Tests.UseCaseFactories.Barbers;

namespace UseCases.Tests.Barbers.GetProfile;
public class GetBarberProfileUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = BarberBuilder.Build();
        var useCase = GetBarberProfileUseCaseFactory.CreateUseCase(loggedUser);

        var result = await useCase.Execute();

        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(loggedUser.Id);
    }

    [Fact]
    public async Task Error_Barber_Not_Found()
    {
        var loggedUser = BarberBuilder.Build();
        var useCase = GetBarberProfileUseCaseFactory.CreateUseCase(loggedUser, barberExists: false);

        var result = await useCase.Execute();

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.NotFound));
        result.Error.Message.Should().Be(ResourceErrorMessages.BARBER_NOT_FOUND);
    }
}
