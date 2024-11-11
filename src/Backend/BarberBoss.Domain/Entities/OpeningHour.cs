namespace BarberBoss.Domain.Entities;
public class OpeningHour : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid BarberShopId { get; set; }
    public virtual BarberShop BarberShop { get; set; } = default!;
    public bool Reserved { get; set; }
}
