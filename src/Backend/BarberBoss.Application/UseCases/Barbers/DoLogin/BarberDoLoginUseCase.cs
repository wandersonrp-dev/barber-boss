using BarberBoss.Communication.Requests.Barber;
using BarberBoss.Communication.Responses.Barber;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BarberBoss.Application.UseCases.Barbers.DoLogin;

public class BarberDoLoginUseCase : IBarberDoLoginUseCase
{
    private readonly IBarberRepository _barberRepository;
    private readonly ILogger<BarberDoLoginUseCase> _logger;
    private readonly IPasswordHasher<Barber> _passwordHasher;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public BarberDoLoginUseCase(IBarberRepository barberRepository, ILogger<BarberDoLoginUseCase> logger, IPasswordHasher<Barber> passwordHasher, IAccessTokenGenerator accessTokenGenerator)
    {
        _barberRepository = barberRepository;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<CustomResult<ResponseBarberDoLoginJson>> Execute(ResquestBarberDoLoginJson request)
    {
        var barber = await _barberRepository.GetByEmailAsync(request.Email);

        if(barber is null)
        {
            _logger.LogError(message: nameof(ErrorCodes.InvalidCredential), args: [ResourceErrorMessages.INVALID_CREDENTIALS]);
            return CustomResult<ResponseBarberDoLoginJson>.Failure(CustomError.InvalidCredential());
        }

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(barber, barber.Password, request.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            _logger.LogError(message: nameof(ErrorCodes.InvalidCredential), args: [ResourceErrorMessages.INVALID_CREDENTIALS]);
            return CustomResult<ResponseBarberDoLoginJson>.Failure(CustomError.InvalidCredential());
        }
        
        var token = _accessTokenGenerator.GenerateToken(barber.Id, barber.Name, barber.Email, barber.UserType);

        return CustomResult<ResponseBarberDoLoginJson>.Success(new ResponseBarberDoLoginJson
        {
            Token = token
        });                
    }
}
