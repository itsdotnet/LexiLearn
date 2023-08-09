namespace LexiLearn.Service.DTOs.Quizzes.QuizHistory;

public class QuizHistoryUpdateDto
{
    public long QuizId { get; set; }

    public long UserId { get; set; }

    public long Score { get; set; }
}
