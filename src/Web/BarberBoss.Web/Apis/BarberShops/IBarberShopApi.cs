using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Responses.BarberShop;
using Refit;

namespace BarberBoss.Web.Apis.BarberShops;
public interface IBarberShopApi
{
    [Post("/api/barber-shops/signup")]
    Task<ApiResponse<ResponseRegisterBarberShopJson>> SignUp(RequestRegisterBarberShopJson request);
}
