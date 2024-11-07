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

    public async Task<bool> ExistsWithSameEmail(string email, Guid barberShopId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();        

        return await context.Barbers
            .AsNoTracking()
            .AnyAsync(e => e.Email.Equals(email) && e.BarberShopId.Equals(barberShopId));        
    }
}
