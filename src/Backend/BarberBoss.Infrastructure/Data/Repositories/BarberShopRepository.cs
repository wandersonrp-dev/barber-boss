﻿using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.Data.Repositories;
public class BarberShopRepository : IBarberShopRepository
{
    private readonly IDbContextFactory<BarberBossDbContext> _contextFactory;

    public BarberShopRepository(IDbContextFactory<BarberBossDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task AddAsync(BarberShop barberShop)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        await context.BarberShops.AddAsync(barberShop);
        
        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsWithSameEmail(string email)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        return await context.BarberShops
            .AsNoTracking()
            .AnyAsync(e => e.Email.Equals(email));
    }

    public async Task<BarberShop?> GetByEmailAsync(string email)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var barberShop = await context.BarberShops
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Email.Equals(email));

        return barberShop;
    }

    public async Task<BarberShop?> GetByIdAsync(Guid id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var barberShop = await context.BarberShops
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id.Equals(id));

        return barberShop;
    }

    public async Task<bool> UpdateAsync(BarberShop barberShop)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        context.BarberShops.Update(barberShop);

        var result = await context.SaveChangesAsync();

        return result > 0;
    }
}