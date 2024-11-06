using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Validators.BarberShop;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BarberBoss.Application.UseCases.BarberShops.Update;
public class UpdateBarberShopUseCase : IUpdateBarberShopUseCase
{
    private readonly ILogger<UpdateBarberShopUseCase> _logger;
    private readonly IBarberShopRepository _barberShopRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IPasswordHasher<BarberShop> _passwordHasher;

    public UpdateBarberShopUseCase(
        ILogger<UpdateBarberShopUseCase> logger, 
        IBarberShopRepository barberShopRepository, 
        ILoggedUser loggedUser, 
        IPasswordHasher<BarberShop> passwordHasher)
    {
        _logger = logger;
        _barberShopRepository = barberShopRepository;
        _loggedUser = loggedUser;
        _passwordHasher = passwordHasher;
    }

    public async Task<CustomResult<bool>> Execute(RequestBarberShopJson request)
    {
        var result = Validate(request);

        if (result.Code == nameof(ErrorCodes.ErrorOnValidation))
        {
            _logger.LogError(message: nameof(ErrorCodes.ErrorOnValidation), args: result.Messages!.ToArray());
            return CustomResult<bool>.Failure(result);
        }

        var loggedUser = await _loggedUser.Get();

        var barberShop = await _barberShopRepository.GetByIdAsync(loggedUser!.Id);

        if(barberShop is null)
        {
            _logger.LogError(message: nameof(ErrorCodes.NotFound), args: [ResourceErrorMessages.BARBER_SHOP_NOT_FOUND]);
            return CustomResult<bool>.Failure(CustomError.NotFound(ResourceErrorMessages.BARBER_SHOP_NOT_FOUND));
        }

        barberShop.Name = request.Name;
        barberShop.Email = request.Email;
        barberShop.Phone = request.Phone;
        barberShop.PhoneContact = request.PhoneContact;
        barberShop.Password = _passwordHasher.HashPassword(barberShop, request.Password);

        var hasBeenUpdated = await _barberShopRepository.UpdateAsync(barberShop);

        if(!hasBeenUpdated)
        {
            _logger.LogError(message: nameof(ErrorCodes.NotFound), 
                args: [$"{ResourceErrorMessages.BARBER_SHOP_NOT_FOUND}: Não foi possível atualizar o perfil da barbearia {barberShop.Name}"]);

            return CustomResult<bool>.Failure(CustomError.InternalServerError());
        }

        return CustomResult<bool>.Success(hasBeenUpdated);
    }

    private static CustomError Validate(RequestBarberShopJson request)
    {
        var validator = new BarberShopValidator();
        var result = validator.Validate(request);

        if(!result.IsValid)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            return CustomError.ErrorOnValidation(errorMessages);
        }

        return CustomError.None;
    }
}
