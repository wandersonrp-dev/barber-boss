using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Communication.Responses.BarberShop;
using Refit;

namespace BarberBoss.Web.Apis.BarberShops;
public interface IBarberShopApi
{
    [Post("/api/barber-shops/signup")]
    Task<ApiResponse<ResponseRegisterBarberShopJson>> SignUp(RequestRegisterBarberShopJson request);
    
    [Post("/api/barber-shops/signin")]
    Task<ApiResponse<ResponseBarberShopDoLoginJson>> SignIn(RequestBarberShopDoLoginJson request);

    [Get("/api/barber-shops/profile")]
    Task<ApiResponse<ResponseBarberShopJson>> GetProfile();

    [Patch("/api/barber-shops/profile")]
    Task<IApiResponse> UpdateProfile(RequestBarberShopJson request);

    [Post("/api/barber-shops/barbers")]
    Task<IApiResponse> CreateBarber(RequestCreateBarberJson request);

    [Get("/api/barber-shops/barbers")]
    Task<IApiResponse<ResponseGetAllBarbersJson>> GetAllBarbers();

    [Post("/api/barber-shops/opening-hours")]
    Task<IApiResponse<ResponseGetAllBarbersJson>> CreateOpeningHours(RequestCreateOpeningHourJson request);

    [Get("/api/barber-shops/opening-hours/{dateFilter}")]
    Task<IApiResponse<ResponseGetOpeningHoursJson>> GetOpeningHours(string dateFilter);
}
