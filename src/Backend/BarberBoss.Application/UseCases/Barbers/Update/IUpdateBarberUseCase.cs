using BarberBoss.Communication.Requests.Barber;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.Barbers.Update;

public interface IUpdateBarberUseCase
{
    Task<CustomResult<bool>> Execute(RequestBarberJson request);
}