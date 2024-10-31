namespace BarberBoss.Domain.UnitOfWork;
public interface IUnitOfWork
{
    Task Commit();
}
