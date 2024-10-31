using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories;
public interface IBarberShopRepository
{    
    Task AddAsync(BarberShop barberShop);        
    Task<bool> ExistsWithSameEmail(string email);    
}
