namespace BarberBoss.Communication.Responses.OpeningHour;
public record ResponseOpeningHourJson
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Reserved { get; set; }    
    public Guid BarberShopId { get; set; }
}
