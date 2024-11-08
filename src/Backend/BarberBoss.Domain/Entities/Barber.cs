﻿namespace BarberBoss.Domain.Entities;
public class Barber : User
{
    public string Phone { get; set; } = string.Empty;
    public Guid BarberShopId { get; set; }
    public virtual BarberShop BarberShop { get; set; } = default!;
}
