using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Communication.Validators.BarberShop;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BarberBoss.Application.UseCases.BarberShops.CreateBarber;
public class CreateBarberUseCase : ICreateBarberUseCase
{
    private readonly IBarberRepository _barberRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly ILogger<CreateBarberUseCase> _logger;
    private readonly IPasswordHasher<Barber> _passwordHasher;

    public CreateBarberUseCase(
        IBarberRepository barberRepository, 
        ILoggedUser loggedUser, 
        ILogger<CreateBarberUseCase> logger, 
        IPasswordHasher<Barber> passwordHasher)
    {
        _barberRepository = barberRepository;
        _loggedUser = loggedUser;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    public async Task<CustomResult<ResponseCreateBarberJson>> Execute(RequestCreateBarberJson request)
    {
        var result = Validate(request);

        if(result.Code == nameof(ErrorCodes.ErrorOnValidation))
        {
            _logger.LogError(message: nameof(ErrorCodes.ErrorOnValidation), args: result.Messages!.ToArray());
            return CustomResult<ResponseCreateBarberJson>.Failure(result);
        }

        var loggedUser = await _loggedUser.Get();
        
        var barberExists = await _barberRepository.ExistsWithSameEmail(request.Email, loggedUser!.Id);

        if(barberExists)
        {
            _logger.LogError(message: $"{nameof(ErrorCodes.Conflict)}: {request.Email}", args: [ResourceErrorMessages.BARBER_ALREADY_EXISTS]);

            return CustomResult<ResponseCreateBarberJson>.Failure(CustomError.Conflict(ResourceErrorMessages.BARBER_ALREADY_EXISTS));
        }

        var barber = new Barber
        { 
            Name = request.Name,
            Email = request.Email,
            UserType = UserType.Barber,
            BarberShopId = loggedUser!.Id,            
        };

        barber.Password = _passwordHasher.HashPassword(barber, request.Password);

        await _barberRepository.AddAsync(barber);

        return CustomResult<ResponseCreateBarberJson>.Success(new ResponseCreateBarberJson(true));
    }

    private static CustomError Validate(RequestCreateBarberJson request)
    {
        var validator = new CreateBarberValidator();
        var result = validator.Validate(request);

        if(!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            return CustomError.ErrorOnValidation(errorMessages);
        }

        return CustomError.None;
    }
}
