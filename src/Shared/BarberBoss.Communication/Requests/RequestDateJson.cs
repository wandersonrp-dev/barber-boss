namespace BarberBoss.Communication.Requests;

public record RequestDateJson
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; } 
    public bool Reserved { get; set; }
}
