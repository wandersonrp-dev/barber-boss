using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Responses.BarberShop;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.BarberShops.GetOpeningHours;

public interface IGetOpeningHoursUseCase
{
    Task<CustomResult<ResponseGetOpeningHoursJson>> Execute(RequestGetOpeningHoursJson request);
}