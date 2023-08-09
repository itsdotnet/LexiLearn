using LexiLearn.DAL.Constexts;
using LexiLearn.DAL.IRepositories;
using LexiLearn.Domain.Entities.Categories;
using LexiLearn.Domain.Entities.Questions;
using LexiLearn.Domain.Entities.Quizzes;
using LexiLearn.Domain.Entities.Words;
using LexiLearn.Domain.Entity.User;

namespace LexiLearn.DAL.Repository;

public class UnitOfWork : IUnitOfWork
{

    private readonly LexiLearnDbContext dbContext;

    public UnitOfWork()
    {
        this.dbContext = new LexiLearnDbContext();

    }

    public IRepository<User> UserRepository { get; }

    public IRepository<Word> WordRepository { get; }

    public IRepository<Quiz> QuizRepository { get; }

    public IRepository<QuizHistory> QuizHistoryRepository { get; }

    public IRepository<Question> QuestionRepository { get; }

    public IRepository<WordCategory> WordCategoryRepository { get; }

    public IRepository<QuizCategory> QuizCategoryRepository { get; }

    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }

    public async Task<bool> SaveAsync()
    {
        return await dbContext.SaveChangesAsync() > 0;
    }
}