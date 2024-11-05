using BarberBoss.Domain.Enums;

namespace BarberBoss.Domain.Entities;
public abstract class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserType UserType { get; set; }
    public UserStatus UserStatus { get; set; } = UserStatus.Active;
}
