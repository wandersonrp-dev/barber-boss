namespace BarberBoss.Communication.Requests.BarberShop;
public record RequestCreateOpeningHourJson
{
    public List<RequestDateJson> OpeningHours { get; set; } = [];
}
