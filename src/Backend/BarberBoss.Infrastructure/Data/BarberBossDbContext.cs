using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.Data;
public class BarberBossDbContext : DbContext
{
    public BarberBossDbContext(DbContextOptions<BarberBossDbContext> options) : base(options)
    {        
    }

    public DbSet<BarberShop> BarberShops { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BarberShop>(entity =>
        {
            entity.Property(e => e.UserType)
                .HasConversion<string>()
                .HasDefaultValue(UserType.BarberShop);
        });
    }
}
