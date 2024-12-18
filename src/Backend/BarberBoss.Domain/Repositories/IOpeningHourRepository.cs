﻿using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories;
public interface IOpeningHourRepository
{
    Task<bool> ExistsByStartAndEndDate(DateTime startDate, DateTime endDate, Guid barberShopId);
    Task AddAsync(OpeningHour openingHour);
    Task AddManyAsync(List<OpeningHour> openingHours);
    Task<List<OpeningHour>> GetByFilteredDate(Guid barberShopId, DateTime dateFiltered);
    Task<OpeningHour?> GetByIdAsync(Guid id, Guid barberShopId);
}
