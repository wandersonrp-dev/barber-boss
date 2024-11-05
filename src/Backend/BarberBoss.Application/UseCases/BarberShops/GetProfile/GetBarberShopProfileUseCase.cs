using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using Microsoft.Extensions.Logging;

namespace BarberBoss.Application.UseCases.BarberShops.GetProfile;
public class GetBarberShopProfileUseCase : IGetBarberShopProfileUseCase
{
    private readonly ILogger<GetBarberShopProfileUseCase> _logger;
    private readonly IBarberShopRepository _barberShopRepository;
    private readonly ILoggedUser _loggedUser;

    public GetBarberShopProfileUseCase(ILogger<GetBarberShopProfileUseCase> logger, IBarberShopRepository barberShopRepository, ILoggedUser loggedUser)
    {
        _logger = logger;
        _barberShopRepository = barberShopRepository;
        _loggedUser = loggedUser;
    }

    public async Task<CustomResult<ResponseBarberShopJson>> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        if(loggedUser is null)
        {
            return CustomResult<ResponseBarberShopJson>.Failure(CustomError.NotFound(ResourceErrorMessages.BARBER_SHOP_NOT_FOUND));
        }

        var barberShop = await _barberShopRepository.GetByIdAsync(loggedUser.Id);

        return CustomResult<ResponseBarberShopJson>.Success(new ResponseBarberShopJson
        { 
            Id = barberShop!.Id,
            Name = barberShop.Name,
            Email = barberShop.Email,
            UserStatus = (UserStatus)barberShop.UserStatus,
            Phone = barberShop.Phone, 
            PhoneContact = barberShop.PhoneContact,
            CreatedAt = barberShop.CreatedAt,
            UpdatedAt = barberShop.UpdatedAt,
            UserType = (UserType)barberShop.UserType,                        
        });        
    }
}
