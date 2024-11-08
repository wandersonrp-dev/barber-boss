namespace BarberBoss.Domain.Entities;
public class WorkBarber : BaseEntity
{
    public Guid BarberId { get; set; }
    public virtual Barber Barber { get; set; } = default!;
    public Guid BarberShopId { get; set; }
    public virtual BarberShop BarberShop { get; set; } = default!;
}
