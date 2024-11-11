using BarberBoss.Communication.Requests.Barber;
using BarberBoss.Communication.Responses.Barber;
using Refit;

namespace BarberBoss.Web.Apis.Barbers;

public interface IBarberApi
{
    [Post("/api/barbers/signin")]
    Task<ApiResponse<ResponseBarberDoLoginJson>> SignIn(RequestBarberDoLoginJson request);

    [Get("/api/barbers/profile")]
    Task<ApiResponse<ResponseBarberJson>> GetProfile();
}
