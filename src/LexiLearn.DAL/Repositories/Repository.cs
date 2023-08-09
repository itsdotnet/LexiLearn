using Microsoft.EntityFrameworkCore;
using LexiLearn.DAL.Constexts;
using LexiLearn.DAL.IRepositories;

namespace LexiLearn.DAL.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly LexiLearnDbContext dbContext;

    private readonly DbSet<T> table;
    public Repository(LexiLearnDbContext dbContext)
    {
        this.dbContext = dbContext;
        table = dbContext.Set<T>();
    }

    public void Add(T entity)
    {
        table.Add(entity);
    }

    public void Delete(T entity)
    {
        table.Remove(entity);
    }

    public T Select(long id)
    {
        return table.Find(id);
    }

    public IQueryable<T> SelectAll()
    {
        return table.AsNoTracking();
    }

    public void Update(T entity)
    {
        dbContext.Entry<T>(entity).State = EntityState.Modified;
    }

    public void SaveChanges()
    {
        dbContext.SaveChanges();
    }
}
