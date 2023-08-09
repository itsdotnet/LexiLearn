using LexiLearn.Domain.Commons;
using LexiLearn.Domain.Entities.Categories;
using LexiLearn.Domain.Enums;

namespace LexiLearn.Domain.Entities.Quizzes;

public class Quiz : Auditable
{
    public string Title { get; set; }

    public string Description { get; set; }

    public int Deadline { get; set; }

    public QuizLevel Level { get; set; }

    public long CategoryId { get; set; }
    public QuizCategory Category { get; set; }
}