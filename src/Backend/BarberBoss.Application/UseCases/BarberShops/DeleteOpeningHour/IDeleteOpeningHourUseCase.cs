using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.BarberShops.DeleteOpeningHour;
public interface IDeleteOpeningHourUseCase
{
    Task<CustomResult<bool>> Execute(Guid openingHourId);
}