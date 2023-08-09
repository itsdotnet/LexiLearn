namespace LexiLearn.DAL.IRepositories;

public interface IUnitOfWork
{
    Task<bool> SaveAsync();
}
