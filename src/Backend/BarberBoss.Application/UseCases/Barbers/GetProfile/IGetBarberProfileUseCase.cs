using BarberBoss.Communication.Responses.Barber;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.Barbers.GetProfile;

public interface IGetBarberProfileUseCase
{
    Task<CustomResult<ResponseBarberJson>> Execute();
}