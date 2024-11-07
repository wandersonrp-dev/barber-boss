using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.BarberShops.CreateBarber;
public interface ICreateBarberUseCase
{
    Task<CustomResult<bool>> Execute(RequestCreateBarberJson request);
}