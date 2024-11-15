using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.Extensions.Logging;

namespace BarberBoss.Application.UseCases.BarberShops.DeleteOpeningHour;
public class DeleteOpeningHourUseCase : IDeleteOpeningHourUseCase
{
    private readonly IOpeningHourRepository _openingHourRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly ILogger<DeleteOpeningHourUseCase> _logger;

    public DeleteOpeningHourUseCase(
        IOpeningHourRepository openingHourRepository, 
        ILoggedUser loggedUser, 
        ILogger<DeleteOpeningHourUseCase> logger)
    {
        _openingHourRepository = openingHourRepository;
        _loggedUser = loggedUser;
        _logger = logger;
    }

    public async Task<CustomResult<bool>> Execute(Guid openingHourId)
    {
        var loggedUser = await _loggedUser.Get();

        var openingHour = await _openingHourRepository.GetByIdAsync(openingHourId, loggedUser!.Id);

        if(openingHour is null)
        {
            _logger.LogError(message: nameof(ErrorCodes.NotFound), args: [ResourceErrorMessages.OPENING_HOUR_NOT_FOUND]);
            return CustomResult<bool>.Failure(CustomError.NotFound(ResourceErrorMessages.OPENING_HOUR_NOT_FOUND));
        }        

        if (openingHour.StartDate < DateTime.UtcNow.AddHours(-3).AddDays(-1))
        {
            _logger.LogError(message: nameof(ErrorCodes.ErrorOnValidation), args: [ResourceErrorMessages.INVALID_OPENING_HOUR_TO_DELETE]);
            return CustomResult<bool>.Failure(CustomError.ErrorOnValidation([ResourceErrorMessages.INVALID_OPENING_HOUR_TO_DELETE]));
        }

        var hasBeenDeleted = await _openingHourRepository.DeleteAsync(openingHour.Id, loggedUser.Id);

        if(!hasBeenDeleted)
        {
            _logger.LogError(message: nameof(ErrorCodes.InternalServerError), args: [ResourceErrorMessages.UNKNOWN_ERROR]);
            return CustomResult<bool>.Failure(CustomError.InternalServerError());
        }

        return CustomResult<bool>.Success(hasBeenDeleted);        
    }
}
