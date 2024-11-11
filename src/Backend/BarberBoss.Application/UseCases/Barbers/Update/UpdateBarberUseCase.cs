using BarberBoss.Communication.Requests.Barber;
using BarberBoss.Communication.Validators.Barber;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BarberBoss.Application.UseCases.Barbers.Update;
public class UpdateBarberUseCase : IUpdateBarberUseCase
{
    private readonly IBarberRepository _barberRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly ILogger<UpdateBarberUseCase> _logger;
    private readonly IPasswordHasher<Barber> _passwordHasher;

    public UpdateBarberUseCase(IBarberRepository barberRepository, ILoggedUser loggedUser, ILogger<UpdateBarberUseCase> logger, IPasswordHasher<Barber> passwordHasher)
    {
        _barberRepository = barberRepository;
        _loggedUser = loggedUser;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    public async Task<CustomResult<bool>> Execute(RequestBarberJson request)
    {
        var validationResult = Validate(request);

        var loggedUser = await _loggedUser.Get();

        var barber = await _barberRepository.GetByIdAsync(loggedUser!.Id);

        if (barber is null)
        {
            _logger.LogError(message: nameof(ErrorCodes.NotFound), args: [ResourceErrorMessages.BARBER_NOT_FOUND]);
            return CustomResult<bool>.Failure(CustomError.NotFound(ResourceErrorMessages.BARBER_NOT_FOUND));
        }

        UpdateBarberFields(barber, request);

        var hasBeenUpdated = await _barberRepository.UpdateAsync(barber);

        if (!hasBeenUpdated)
        {
            _logger.LogError(message: nameof(ErrorCodes.NotFound),
                args: [$"{ResourceErrorMessages.BARBER_SHOP_NOT_FOUND}: Não foi possível atualizar o perfil do(a) barbeiro(a) {barber.Name}"]);                

            return CustomResult<bool>.Failure(CustomError.InternalServerError());
        }

        return CustomResult<bool>.Success(hasBeenUpdated);        
    }

    private static CustomError Validate(RequestBarberJson request)
    {
        var validator = new BarberValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {

            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            return CustomError.ErrorOnValidation(errorMessages);
        }

        return CustomError.None;
    }

    private void UpdateBarberFields(Barber barber, RequestBarberJson request)
    {
        if (!string.IsNullOrWhiteSpace(request.Name))
            barber.Name = request.Name;

        if (!string.IsNullOrWhiteSpace(request.Email))
            barber.Email = request.Email;

        if (!string.IsNullOrWhiteSpace(request.Phone))
            barber.Phone = request.Phone;                

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            barber.Password = _passwordHasher.HashPassword(barber, request.Password);
            barber.ChangedInitialPassword = true;
        }            
    }
}
