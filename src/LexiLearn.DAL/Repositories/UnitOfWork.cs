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

    public UnitOfWork(LexiLearnDbContext dbContext)
    {
        this.dbContext = dbContext;
        UserRepository = new Repository<User>(dbContext);
        WordRepository = new Repository<Word>(dbContext);
        QuizRepository = new Repository<Quiz>(dbContext);
        QuizHistoryRepository = new Repository<QuizHistory>(dbContext);
        QuestionRepository = new Repository<Question>(dbContext);
        WordCategoryRepository = new Repository<WordCategory>(dbContext);
        QuizCategoryRepository = new Repository<QuizCategory>(dbContext);
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