using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Infrastructure.Data.Repositories;
public class BarberShopRepository : IBarberShopRepository
{
    private readonly BarberBossDbContext _context;

    public BarberShopRepository(BarberBossDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(BarberShop barberShop)
    {
        
    }

    public Task<bool> ExistsWithSameEmail(string email)
    {
        throw new NotImplementedException();
    }
}
