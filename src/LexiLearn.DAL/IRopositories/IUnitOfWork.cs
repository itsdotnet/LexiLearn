using LexiLearn.Domain.Entities.Categories;
using LexiLearn.Domain.Entities.Questions;
using LexiLearn.Domain.Entities.Quizzes;
using LexiLearn.Domain.Entities.Words;
using LexiLearn.Domain.Entity.User;

namespace LexiLearn.DAL.IRepositories;

public interface IUnitOfWork
{
    IRepository<User> UserRepository { get; }

    IRepository<Word> WordRepository { get; }

    IRepository<Quiz> QuizRepository { get; }

    IRepository<QuizHistory> QuizHistoryRepository { get; }

    IRepository<Question> QuestionRepository { get; }

    IRepository<WordCategory> WordCategoryRepository { get; }

    IRepository<QuizCategory> QuizCategoryRepository { get; }

    Task<bool> SaveAsync();
}
