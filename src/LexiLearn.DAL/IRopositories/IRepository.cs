namespace LexiLearn.DAL.IRepositories;

public interface IRepository<T> where T : class
{
    void Add(T entity);

    void Update(T entity);

    void Delete(T entity);

    T Select(long id);

    IQueryable<T> SelectAll();

    void SaveChanges();
}
