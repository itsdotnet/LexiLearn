using LexiLearn.Domain.Enums;

namespace LexiLearn.Service.DTOs.Quizzes;

public class QuizResultDto
{
    public long Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int Deadline { get; set; }

    public QuizLevel Level { get; set; }

    public long CategoryId { get; set; }
}