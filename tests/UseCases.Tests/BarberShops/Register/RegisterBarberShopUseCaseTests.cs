using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;
using UseCases.Tests.UseCaseFactories.BarberShops;

namespace UseCases.Tests.BarberShops.Register;
public class RegisterBarberShopUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterBarberShopJsonBuilder.Build();
        
        var useCase = RegisterBarberShopUseCaseFactory.CreateUseCase();

        var result = await useCase.Execute(request);

        result.Value.Should().NotBeNull();
        result.Value.IsCreated.Should().Be(true);        
    }

    [Fact]
    public async Task Error_Required_Name()
    {
        var request = RequestRegisterBarberShopJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = RegisterBarberShopUseCaseFactory.CreateUseCase();

        var result = await useCase.Execute(request);      

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.ErrorOnValidation));
        result.Error.Messages.Should().ContainSingle().And.Contain(error => error.Equals(ResourceErrorMessages.REQUIRED_NAME));
    }

    [Fact]
    public async Task Error_Barber_Shop_Already_Exists()
    {
        var request = RequestRegisterBarberShopJsonBuilder.Build();        

        var useCase = RegisterBarberShopUseCaseFactory.CreateUseCase(request.Email);

        var result = await useCase.Execute(request);
        
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.Conflict));
        result.Error.Messages.Should().ContainSingle().And.Contain(error => error.Equals(ResourceErrorMessages.BARBER_SHOP_ALREADY_EXISTS));
    }    
}
