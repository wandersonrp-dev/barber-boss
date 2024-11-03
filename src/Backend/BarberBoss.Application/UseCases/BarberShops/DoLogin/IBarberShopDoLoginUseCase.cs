using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.BarberShops.DoLogin;
public interface IBarberShopDoLoginUseCase
{
    Task<CustomResult<ResponseBarberShopDoLoginJson>> Execute(RequestBarberShopDoLoginJson request);
}