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

        if(validationResult.Code == nameof(ErrorCodes.ErrorOnValidation))
        {
            return CustomResult<bool>.Failure(validationResult);
        }

        var loggedUser = await _loggedUser.Get();

        var barberShop = await _barberShopRepository.GetByIdAsync(loggedUser!.Id);
        
        if(barberShop is null)
        {
            return CustomResult<bool>.Failure(CustomError.NotFound(ResourceErrorMessages.BARBER_SHOP_NOT_FOUND));
        }

        var openingHourExists = await _openingHourRepository.ExistsByStartAndEndDate(request.StartDate, request.EndDate, loggedUser.Id);

        if (openingHourExists)
        {
            return CustomResult<bool>.Failure(CustomError.Conflict(ResourceErrorMessages.OPENING_HOUR_ALREADY_EXISTS));
        }

        var openingHour = new OpeningHour
        {
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            BarberShopId = loggedUser!.Id,     
            Reserved = request.Reserved,
        };

        await _openingHourRepository.AddAsync(openingHour);

        return CustomResult<bool>.Success(true);
    }

    private static CustomError Validate(RequestCreateOpeningHourJson request)
    {
        var validator = new CreateOpeningHourValidator();
        var result = validator.Validate(request);

        if(!result.IsValid)
        {
            var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();

            return CustomError.ErrorOnValidation(errorMessage);
        }

        return CustomError.None;
    }
}