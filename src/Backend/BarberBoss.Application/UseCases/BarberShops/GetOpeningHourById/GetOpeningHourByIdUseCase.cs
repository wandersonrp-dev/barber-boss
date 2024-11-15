using BarberBoss.Communication.Responses.OpeningHour;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.Extensions.Logging;

namespace BarberBoss.Application.UseCases.BarberShops.GetOpeningHourById;
public class GetOpeningHourByIdUseCase : IGetOpeningHourByIdUseCase
{
    private readonly IOpeningHourRepository _openingHourRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly ILogger<GetOpeningHourByIdUseCase> _logger;

    public GetOpeningHourByIdUseCase(IOpeningHourRepository openingHourRepository, ILoggedUser loggedUser, ILogger<GetOpeningHourByIdUseCase> logger)
    {
        _openingHourRepository = openingHourRepository;
        _loggedUser = loggedUser;
        _logger = logger;
    }

    public async Task<CustomResult<ResponseOpeningHourJson>> Execute(Guid id)
    {
        var loggedUser = await _loggedUser.Get();

        var openingHour = await _openingHourRepository.GetByIdAsync(id, loggedUser!.Id);

        if (openingHour is null)
        {
            _logger.LogError(message: nameof(ErrorCodes.NotFound), args: [ResourceErrorMessages.OPENING_HOUR_NOT_FOUND]);
            return CustomResult<ResponseOpeningHourJson>.Failure(CustomError.NotFound(ResourceErrorMessages.OPENING_HOUR_NOT_FOUND));
        }

        return CustomResult<ResponseOpeningHourJson>.Success(new ResponseOpeningHourJson
        {
            Id = openingHour.Id,
            BarberShopId = openingHour.BarberShopId,
            StartDate = openingHour.StartDate,
            EndDate = openingHour.EndDate,
            Reserved = openingHour.Reserved
        });
    }
}
