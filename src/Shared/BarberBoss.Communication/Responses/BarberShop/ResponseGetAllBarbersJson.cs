using BarberBoss.Communication.Responses.Barber;

namespace BarberBoss.Communication.Responses.BarberShop;

public record ResponseGetAllBarbersJson
{
    public List<ResponseBarberJson> Barbers { get; set; } = new();    
}
