using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.BarberShops.CreateBarber;
public interface ICreateBarberUseCase
{
    Task<CustomResult<ResponseCreateBarberJson>> Execute(RequestCreateBarberJson request);
}