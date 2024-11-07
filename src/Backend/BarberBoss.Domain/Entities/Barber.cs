namespace BarberBoss.Domain.Entities;
public class Barber : User
{
    public Guid BarberShopId { get; set; }
    public virtual BarberShop BarberShop { get; set; } = default!;
}
