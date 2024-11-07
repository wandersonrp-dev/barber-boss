﻿using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.Data;
public class BarberBossDbContext : DbContext
{
    public BarberBossDbContext(DbContextOptions<BarberBossDbContext> options) : base(options)
    {        
    }

    public DbSet<BarberShop> BarberShops { get; set; }
    public DbSet<Barber> Barbers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<User>();

        modelBuilder.Entity<BarberShop>(entity =>
        {
            entity.Property(e => e.UserType)
                .HasConversion<string>()
                .HasDefaultValue(UserType.BarberShop);

            entity.Property(e => e.UserStatus)
                .HasConversion<string>()
                .HasDefaultValue(UserStatus.Active);

            entity.HasMany(e => e.Barbers)
                .WithOne(e => e.BarberShop)
                .HasForeignKey(e => e.BarberShopId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Barber>(entity =>
        {
            entity.Property(e => e.UserType)
                .HasConversion<string>()
                .HasDefaultValue(UserType.Barber);

            entity.Property(e => e.UserStatus)
                .HasConversion<string>()
                .HasDefaultValue(UserStatus.Active);
        });
    }
}
