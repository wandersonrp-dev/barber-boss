using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.BarberShops.CreateOpeningHour;
public interface ICreateOpeningHourUseCase
{
    Task<CustomResult<bool>> Execute(RequestCreateOpeningHourJson request);
}