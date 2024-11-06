using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.BarberShops.Update;

public interface IUpdateBarberShopUseCase
{
    Task<CustomResult<bool>> Execute(RequestBarberShopJson request);
}