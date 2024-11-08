namespace BarberBoss.Communication.Requests.Barber;

public record ResquestBarberDoLoginJson
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
