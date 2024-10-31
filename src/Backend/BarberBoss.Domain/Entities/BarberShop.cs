using BarberBoss.Domain.Enums;

namespace BarberBoss.Domain.Entities;
public class BarberShop : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PhoneContact { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserType UserType { get; set; } = UserType.BarberShop;
}
