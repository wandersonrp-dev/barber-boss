using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.BarberShops.Register;
public interface IRegisterBarberShopUseCase
{
    Task<CustomResult<ResponseRegisterBarberShopJson>> Execute(RequestRegisterBarberShopJson request);
}
