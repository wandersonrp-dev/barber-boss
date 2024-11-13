using BarberBoss.Communication.Responses.OpeningHour;

namespace BarberBoss.Communication.Responses.BarberShop;
public record ResponseGetOpeningHoursJson
{
    public List<ResponseOpeningHourJson> OpeningHours { get; set; } = [];
}
