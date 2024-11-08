namespace BarberBoss.Domain.Entities;
public class BarberShop : User
{    
    public string Phone { get; set; } = string.Empty;
    public string PhoneContact { get; set; } = string.Empty;
    public virtual ICollection<WorkBarber> WorkBarbers { get; set; }

    public BarberShop()
    {
        WorkBarbers = [];
    }
}