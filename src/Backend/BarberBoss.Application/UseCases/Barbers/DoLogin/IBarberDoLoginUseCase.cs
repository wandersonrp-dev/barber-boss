using BarberBoss.Communication.Requests.Barber;
using BarberBoss.Communication.Responses.Barber;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.Barbers.DoLogin;

public interface IBarberDoLoginUseCase
{
    Task<CustomResult<ResponseBarberDoLoginJson>> Execute(ResquestBarberDoLoginJson request);
}
