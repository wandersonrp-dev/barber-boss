using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BarberBoss.Application.UseCases.BarberShops.DoLogin;
public class BarberShopDoLoginUseCase : IBarberShopDoLoginUseCase
{
    private readonly IBarberShopRepository _barberShopRepository;
    private readonly ILogger<BarberShopDoLoginUseCase> _logger;
    private readonly IPasswordHasher<BarberShop> _passwordHasher;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public BarberShopDoLoginUseCase(
        IBarberShopRepository barberShopRepository, 
        ILogger<BarberShopDoLoginUseCase> logger, 
        IPasswordHasher<BarberShop> passwordHasher, 
        IAccessTokenGenerator accessTokenGenerator)
    {
        _barberShopRepository = barberShopRepository;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<CustomResult<ResponseBarberShopDoLoginJson>> Execute(RequestBarberShopDoLoginJson request)
    {        
        var barberShop = await _barberShopRepository.GetByEmailAsync(request.Email);

        if(barberShop is null)
        {
            _logger.LogError(message: nameof(ErrorCodes.InvalidCredential), args: [ResourceErrorMessages.INVALID_CREDENTIALS]);
            return CustomResult<ResponseBarberShopDoLoginJson>.Failure(CustomError.InvalidCredential());
        }

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(barberShop, barberShop.Password, request.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            _logger.LogError(message: nameof(ErrorCodes.InvalidCredential), args: [ResourceErrorMessages.INVALID_CREDENTIALS]);
            return CustomResult<ResponseBarberShopDoLoginJson>.Failure(CustomError.InvalidCredential());
        }
        
        var token = _accessTokenGenerator.GenerateToken(barberShop.Id, barberShop.Name, barberShop.Email, barberShop.UserType);

        return CustomResult<ResponseBarberShopDoLoginJson>.Success(new ResponseBarberShopDoLoginJson
        {
            Token = token
        });        
    }
}
