using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.BarberShops.GetProfile;

public interface IGetBarberShopProfileUseCase
{
    Task<CustomResult<ResponseBarberShopJson>> Execute();
}