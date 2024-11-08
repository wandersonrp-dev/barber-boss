using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Responses.Barber;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.BarberShops.GetAllBarbers;
public class GetAllBarbersUseCase : IGetAllBarbersUseCase
{
    private readonly IBarberRepository _barberRepository;
    private readonly ILoggedUser _loggedUser;    

    public GetAllBarbersUseCase(IBarberRepository barberRepository, ILoggedUser loggedUser)
    {
        _barberRepository = barberRepository;
        _loggedUser = loggedUser;        
    }

    public async Task<CustomResult<ResponseGetAllBarbersJson>> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var barbers = await _barberRepository.GetAllBarbersAsync(loggedUser!.Id);
        
        return CustomResult<ResponseGetAllBarbersJson>.Success(new ResponseGetAllBarbersJson 
        {
            Barbers = barbers.Select(barber => new ResponseBarberJson {
                Id = barber.Id,
                Name = barber.Name,
                Email = barber.Email,
                Phone = barber.Phone,
                CreatedAt = barber.CreatedAt,
                UserStatus = (UserStatus)barber.UserStatus,
            }).ToList()
        });            
    }
}
