using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Common.Tests.Utilities.Entities;
using Common.Tests.Utilities.Requests.Barbers;
using FluentAssertions;
using UseCases.Tests.UseCaseFactories.Barbers;

namespace UseCases.Tests.Barbers.DoLogin;
public class BarberDoLoginUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var barber = BarberBuilder.Build();

        var request = RequestBarberDoLoginJsonBuilder.Build(barber);

        var useCase = BarberDoLoginUseCaseFactory.CreateUseCase(barber, barber.Password);

        var result = await useCase.Execute(request);

        result.IsSuccess.Should().BeTrue();
        result.Value.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Email_Invalid_Credentials()
    {
        var barber = BarberBuilder.Build();

        var request = RequestBarberDoLoginJsonBuilder.Build();

        var useCase = BarberDoLoginUseCaseFactory.CreateUseCase(barber, barber.Password);

        var result = await useCase.Execute(request);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.InvalidCredential));
        result.Error.Message.Should().Be(ResourceErrorMessages.INVALID_CREDENTIALS);
    }

    [Fact]
    public async Task Error_Password_Match_Invalid_Credentials()
    {
        var barber = BarberBuilder.Build();

        var request = RequestBarberDoLoginJsonBuilder.Build(barber);

        var useCase = BarberDoLoginUseCaseFactory.CreateUseCase(barber, request.Password);

        var result = await useCase.Execute(request);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.InvalidCredential));
        result.Error.Message.Should().Be(ResourceErrorMessages.INVALID_CREDENTIALS);
    }
}
