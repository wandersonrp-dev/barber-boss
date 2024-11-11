using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.Data.Repositories;
public class OpeningHourRepository : IOpeningHourRepository
{
    private readonly IDbContextFactory<BarberBossDbContext> _contextFactory;

    public OpeningHourRepository(IDbContextFactory<BarberBossDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task AddAsync(OpeningHour openingHour)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        await context.OpeningHours.AddAsync(openingHour);

        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByStartAndEndDate(DateTime startDate, DateTime endDate, Guid barberShopId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        return await context.OpeningHours
            .AsNoTracking()
            .AnyAsync(e => 
                e.StartDate.Equals(startDate) && 
                e.EndDate.Equals(endDate) && 
                e.BarberShopId.Equals(barberShopId));        
    }
}
