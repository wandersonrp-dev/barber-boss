using System.Text.Json.Serialization;

namespace BarberBoss.Communication.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserStatus
{
    Active,
    Inactive,
    Suspended,
    Awaiting,
}
