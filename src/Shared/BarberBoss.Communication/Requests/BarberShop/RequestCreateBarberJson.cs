using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Requests.BarberShop;
public record RequestCreateBarberJson
{    
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public UserStatus UserStatus { get; set; } = UserStatus.Active;
}
