namespace BarberBoss.Communication.Requests.BarberShop;
public record RequestCreateOpeningHourJson
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }        
    public bool Reserved { get; set; }
}
