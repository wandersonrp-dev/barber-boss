using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;
using UseCases.Tests.Cryptography;
using UseCases.Tests.Logger;
using UseCases.Tests.Repositories;

namespace UseCases.Tests.BarberShops.Register;
public class RegisterBarberShopUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterBarberShopJsonBuilder.Build();
        
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Value.Should().NotBeNull();
        result.Value.IsCreated.Should().Be(true);        
    }

    [Fact]
    public async Task Error_Required_Name()
    {
        var request = RequestRegisterBarberShopJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);      

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(nameof(ErrorCodes.ErrorOnValidation));
        result.Error.Messages.Should().ContainSingle().And.Contain(error => error.Equals(ResourceErrorMessages.REQUIRED_NAME));
    }

    [Fact]
    public async Task Error_Barber_Shop_Already_Exists()
    {
        var request = RequestRegisterBarberShopJsonBuilder.Build();        

        var useCase = CreateUseCase(request.Email);

        var result = await useCase.Execute(request);
    }

    private static RegisterBarberShopUseCase CreateUseCase(string? email = null)
    {
        var barberShopRepository = new BarberShopRepositoryBuilder();
        var logger = LoggerBuilder<RegisterBarberShopUseCase>.Build();
        var passwordHasher = PasswordHasherBuilder<BarberShop>.Build();

        if(!string.IsNullOrWhiteSpace(email))
        {
            barberShopRepository.ExistsWithSameEmail(email);
        }

        return new RegisterBarberShopUseCase(barberShopRepository.Build(), logger, passwordHasher);
    }
}
