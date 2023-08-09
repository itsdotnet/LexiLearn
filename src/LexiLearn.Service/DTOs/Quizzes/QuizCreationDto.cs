using LexiLearn.Domain.Enums;

namespace LexiLearn.Service.DTOs.Quizzes;

public class QuizCreationDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public int Deadline { get; set; }

    public QuizLevel Level { get; set; }

    public long CategoryId { get; set; }
}