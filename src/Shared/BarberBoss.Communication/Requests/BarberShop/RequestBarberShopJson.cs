﻿namespace BarberBoss.Communication.Requests.BarberShop;
public record RequestBarberShopJson
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PhoneContact { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;    
}
