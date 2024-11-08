using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.Data.Repositories;
public class BarberRepository : IBarberRepository
{
    private readonly IDbContextFactory<BarberBossDbContext> _contextFactory;

    public BarberRepository(IDbContextFactory<BarberBossDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task AddAsync(Barber barber)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        await context.AddAsync(barber);

        await context.SaveChangesAsync();                        
    }

    public async Task<bool> ExistsWithSameEmail(string email)
    {
        using var context = await _contextFactory.CreateDbContextAsync();        

        return await context.Barbers
            .AsNoTracking()
            .AnyAsync(e => e.Email.Equals(email));        
    }

    public async Task<List<Barber>> GetAllBarbersAsync(Guid barberShopId)
    {
        using var context = _contextFactory.CreateDbContext();

        return await context.Barbers
            .AsNoTracking()
            .Include(e => e.WorkBarbers)
            .ThenInclude(e => e.BarberShop)
            .Where(e => e.WorkBarbers.Any(wb => wb.BarberShopId == barberShopId))
            .ToListAsync();
    }

    public async Task<Barber?> GetByEmailAsync(string email)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        return await context.Barbers
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Email.Equals(email));
    }
}
