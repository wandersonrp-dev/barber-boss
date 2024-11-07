using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories;
public interface IBarberRepository
{
    Task AddAsync(Barber barber);
    Task<bool> ExistsWithSameEmail(string email, Guid barberShopId);
}
