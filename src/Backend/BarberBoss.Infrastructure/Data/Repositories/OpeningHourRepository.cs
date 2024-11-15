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

    public async Task AddManyAsync(List<OpeningHour> openingHours)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        await context.OpeningHours.AddRangeAsync(openingHours);

        await context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id, Guid barberShopId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var rows = await context.OpeningHours
            .Where(e => e.Id == id && e.BarberShopId == barberShopId)
            .ExecuteDeleteAsync();

        return rows > 0;        
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

    public async Task<List<OpeningHour>> GetByFilteredDate(Guid barberShopId, DateTime dateFiltered)
    {
        var dateFilteredUtc = dateFiltered.ToUniversalTime().AddHours(-3);

        using var context = await _contextFactory.CreateDbContextAsync();

        return await context.OpeningHours
            .AsNoTracking()
            .OrderBy(e => e.StartDate)
            .Where(e => e.BarberShopId == barberShopId && e.StartDate.Date == dateFilteredUtc.Date)
            .ToListAsync();
    }

    public async Task<OpeningHour?> GetByIdAsync(Guid id, Guid barberShopId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        return await context.OpeningHours
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id && e.BarberShopId == barberShopId);
    }
}
