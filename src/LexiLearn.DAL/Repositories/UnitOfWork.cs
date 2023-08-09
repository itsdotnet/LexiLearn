using LexiLearn.DAL.Constexts;
using LexiLearn.DAL.IRepositories;

namespace LexiLearn.DAL.Repository;

public class UnitOfWork : IUnitOfWork
{

    private readonly LexiLearnDbContext dbContext;

    public UnitOfWork()
    {
        this.dbContext = new LexiLearnDbContext();

    }

    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }

    public async Task<bool> SaveAsync()
    {
        return await dbContext.SaveChangesAsync() > 0;
    }
}