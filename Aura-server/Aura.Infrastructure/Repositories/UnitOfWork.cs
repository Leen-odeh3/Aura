using Aura.Domain.Contracts;
using Aura.Infrastructure.Data;

namespace Aura.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<int> SaveChangesAsync()
    {

        return await context.SaveChangesAsync();
    }
}