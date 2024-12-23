namespace Aura.Domain.Contracts;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}