namespace BarberBoss.Domain.Entities;
public class Barber : User
{
    public string Phone { get; set; } = string.Empty;
    public bool ChangedInitialPassword { get; set; }
    public virtual ICollection<WorkBarber> WorkBarbers { get; set; }

    public Barber()
    {
        WorkBarbers = [];
    }
}
