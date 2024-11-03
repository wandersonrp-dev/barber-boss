namespace BarberBoss.Communication.Requests.BarberShop;
public record RequestBarberShopDoLoginJson
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
