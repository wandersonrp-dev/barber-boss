using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories;
public interface IBarberRepository
{
    Task AddAsync(Barber barber);
    Task<bool> ExistsWithSameEmail(string email);
    Task<Barber?> GetByEmailAsync(string email);
    Task<List<Barber>> GetAllBarbersAsync(Guid barberShopId);
    Task<Barber?> GetByIdAsync(Guid id);
    Task<bool> UpdateAsync(Barber barber);
}
