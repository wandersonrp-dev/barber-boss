using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Communication.Responses.OpeningHour;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.Extensions.Logging;

namespace BarberBoss.Application.UseCases.BarberShops.GetOpeningHours;
public class GetOpeningHoursUseCase : IGetOpeningHoursUseCase
{    
    private readonly IOpeningHourRepository _openingHourRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly ILogger<GetOpeningHoursUseCase> _logger;

    public GetOpeningHoursUseCase(IOpeningHourRepository openingHourRepository, ILoggedUser loggedUser, ILogger<GetOpeningHoursUseCase> logger)
    {        
        _openingHourRepository = openingHourRepository;
        _loggedUser = loggedUser;
        _logger = logger;
    }

    public async Task<CustomResult<ResponseGetOpeningHoursJson>> Execute(RequestGetOpeningHoursJson request)
    {
        var loggedUser = await _loggedUser.Get();

        if(loggedUser is null)
        {
            _logger.LogError(message: nameof(ErrorCodes.NotFound), args: [ResourceErrorMessages.BARBER_SHOP_NOT_FOUND]);
            return CustomResult<ResponseGetOpeningHoursJson>.Failure(CustomError.NotFound(ResourceErrorMessages.BARBER_SHOP_NOT_FOUND));
        }

        var openingHours = await _openingHourRepository.GetByFilteredDate(loggedUser.Id, request.DateFilter);

        return CustomResult<ResponseGetOpeningHoursJson>.Success(new ResponseGetOpeningHoursJson
        {
            OpeningHours = openingHours.Select(openingHour => new ResponseOpeningHourJson
            {
                Id = openingHour.Id,
                StartDate = openingHour.StartDate,
                EndDate = openingHour.EndDate,
                Reserved = openingHour.Reserved,
                BarberShopId = openingHour.BarberShopId
            }).ToList()
        });        
    }
}
