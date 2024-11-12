using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Validators.BarberShop;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.Extensions.Logging;

namespace BarberBoss.Application.UseCases.BarberShops.CreateOpeningHour;
public class CreateOpeningHourUseCase : ICreateOpeningHourUseCase
{
    private readonly IBarberShopRepository _barberShopRepository;
    private readonly IOpeningHourRepository _openingHourRepository;
    private readonly ILogger<CreateOpeningHourUseCase> _logger;
    private readonly ILoggedUser _loggedUser;

    public CreateOpeningHourUseCase(
        IBarberShopRepository barberShopRepository,
        IOpeningHourRepository openingHourRepository,
        ILogger<CreateOpeningHourUseCase> logger,
        ILoggedUser loggedUser)
    {
        _barberShopRepository = barberShopRepository;
        _openingHourRepository = openingHourRepository;
        _logger = logger;
        _loggedUser = loggedUser;
    }

    public async Task<CustomResult<bool>> Execute(RequestCreateOpeningHourJson request)
    {
        var validationResult = Validate(request);

        if (validationResult.Code == nameof(ErrorCodes.ErrorOnValidation))
        {
            return CustomResult<bool>.Failure(validationResult);
        }

        var loggedUser = await _loggedUser.Get();

        var barberShop = await _barberShopRepository.GetByIdAsync(loggedUser!.Id);

        if (barberShop is null)
        {
            return CustomResult<bool>.Failure(CustomError.NotFound(ResourceErrorMessages.BARBER_SHOP_NOT_FOUND));
        }

        List<OpeningHour> openingHours = []; 

        foreach (var openingHour in request.OpeningHours)
        {
            var openingHourExists = await _openingHourRepository.ExistsByStartAndEndDate(openingHour.StartDate, openingHour.EndDate, loggedUser.Id);

            if (openingHourExists)
            {
                return CustomResult<bool>.Failure(CustomError.Conflict(ResourceErrorMessages.OPENING_HOUR_ALREADY_EXISTS));
            }            

            openingHours.Add(new OpeningHour
            {
                StartDate = openingHour.StartDate,
                EndDate = openingHour.EndDate,
                BarberShopId = loggedUser.Id,
                Reserved = openingHour.Reserved,
            });            
        }

        await _openingHourRepository.AddManyAsync(openingHours);

        return CustomResult<bool>.Success(true);
    }

    private static CustomError Validate(RequestCreateOpeningHourJson request)
    {
        var validator = new CreateOpeningHourValidator();                

        foreach (var openingHour in request.OpeningHours)
        {
            var result = validator.Validate(openingHour);

            if (!result.IsValid)
            {                
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                return CustomError.ErrorOnValidation(errorMessages);
            }            
        }        

        return CustomError.None;
    }
}    