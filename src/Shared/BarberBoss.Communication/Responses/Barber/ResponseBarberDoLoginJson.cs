namespace BarberBoss.Communication.Responses.Barber;

public record ResponseBarberDoLoginJson
{
    public string Token { get; set; } = string.Empty;
}
