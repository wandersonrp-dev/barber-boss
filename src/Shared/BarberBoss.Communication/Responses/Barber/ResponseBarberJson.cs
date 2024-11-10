using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Responses.Barber;
public record ResponseBarberJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool ChangedInitialPassword { get; set; }
    public UserStatus UserStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
