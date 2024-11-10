using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Responses.Barber;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.Extensions.Logging;

namespace BarberBoss.Application.UseCases.Barbers.GetProfile;
public class GetBarberProfileUseCase : IGetBarberProfileUseCase
{
    private readonly IBarberRepository _barberRepository;
    private readonly ILogger<GetBarberProfileUseCase> _logger;
    private readonly ILoggedUser _loggedUser;

    public GetBarberProfileUseCase(
        IBarberRepository barberRepository, 
        ILogger<GetBarberProfileUseCase> logger, 
        ILoggedUser loggedUser)
    {
        _barberRepository = barberRepository;
        _logger = logger;
        _loggedUser = loggedUser;
    }

    public async Task<CustomResult<ResponseBarberJson>> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var barber = await _barberRepository.GetByIdAsync(loggedUser!.Id);

        if(barber is null)
        {
            _logger.LogError(message: nameof(ErrorCodes.NotFound), args: [ResourceErrorMessages.BARBER_NOT_FOUND]);
            return CustomResult<ResponseBarberJson>.Failure(CustomError.NotFound(ResourceErrorMessages.BARBER_NOT_FOUND));
        }

        return CustomResult<ResponseBarberJson>.Success(new ResponseBarberJson
        {
            Id = barber.Id,
            Name = barber.Name,
            Email = barber.Email,
            Phone = barber.Phone,
            CreatedAt = barber.CreatedAt,
            UpdatedAt = barber.UpdatedAt,
            UserStatus = (UserStatus)barber.UserStatus,
            ChangedInitialPassword = barber.ChangedInitialPassword,
        });
    }
}
