using LexiLearn.Domain.Commons;
using LexiLearn.Domain.Entity.User;

namespace LexiLearn.Domain.Entities.Quizzes;

public class QuizHistory : Auditable
{
    public long QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public long Score { get; set; }
}
