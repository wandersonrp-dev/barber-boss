using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Services.LoggedUser;
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
            return CustomResult<ResponseBarberShopJson>.Failure(CustomError.NotFound(""));
        }

        return CustomResult<ResponseBarberShopJson>.Success(new ResponseBarberShopJson
        { 
            Id = loggedUser.Id,
            Name = loggedUser.Name,
            Email = loggedUser.Email,
            UserStatus = (UserStatus)loggedUser.UserStatus,
            CreatedAt = loggedUser.CreatedAt,
            UpdatedAt = loggedUser.UpdatedAt,
            UserType = (UserType)loggedUser.UserType,                        
        });        
    }
}
