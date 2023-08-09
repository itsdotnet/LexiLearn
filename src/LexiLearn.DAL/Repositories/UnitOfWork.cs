using QueHub.DAL.Constexts;
using QueHub.DAL.IRepositories;
using QueHub.Domain.Entity.AnswerDislikes;
using QueHub.Domain.Entity.AnswerLikes;
using QueHub.Domain.Entity.Answers;
using QueHub.Domain.Entity.Category;
using QueHub.Domain.Entity.QuestionDislikes;
using QueHub.Domain.Entity.QuestionLikes;
using QueHub.Domain.Entity.Questions;
using QueHub.Domain.Entity.User;

namespace BermudTravel.DAL.Repository;

public class UnitOfWork : IUnitOfWork
{

    private readonly LexiLearnDbContext dbContext;

    public UnitOfWork()
    {
        this.dbContext = new LexiLearnDbContext();

    }

    public IRepository<UserEntity> UserRepository { get; }

    public IRepository<QuestionEntity> QuestionRepository { get; }

    public IRepository<AnswerEntity> AnswerRepository { get; }

    public IRepository<CategoryEntity> CategoryRepository { get; }

    public IRepository<QuestionLikeEntity> QuestionLikeRepository { get; }

    public IRepository<QuestionDislikeEntity> QuestionDislikeRepository { get; }

    public IRepository<AnswerLikeEntity> AnswerLikeRepository { get; }

    public IRepository<AnswerDislikeEntity> AnswerDislikeRepository { get; }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<bool> SaveAsync()
    {
        return await dbContext.SaveChangesAsync() > 0;
    }
}