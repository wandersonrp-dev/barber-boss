using BarberBoss.Communication.Responses.Barber;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.BarberShops.GetAllBarbers;

public interface IGetAllBarbersUseCase
{
    Task<CustomResult<ResponseGetAllBarbersJson>> Execute();
}
