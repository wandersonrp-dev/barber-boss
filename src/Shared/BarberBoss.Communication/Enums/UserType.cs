using System.Text.Json.Serialization;

namespace BarberBoss.Communication.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserType
{
    BarberShop,
    Customer,
    Barber
}
