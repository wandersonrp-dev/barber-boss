namespace BarberBoss.Communication.Requests.BarberShop;
public record RequestGetOpeningHoursJson
{
    public DateTime DateFilter { get; set; }
}
