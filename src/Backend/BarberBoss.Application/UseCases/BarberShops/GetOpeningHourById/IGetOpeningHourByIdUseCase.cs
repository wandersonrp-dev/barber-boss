using BarberBoss.Communication.Responses.OpeningHour;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.BarberShops.GetOpeningHourById;

public interface IGetOpeningHourByIdUseCase
{
    Task<CustomResult<ResponseOpeningHourJson>> Execute(Guid id);
}