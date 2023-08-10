using LexiLearn.Domain.Commons;
using LexiLearn.Domain.Entities.Quizzes;
using LexiLearn.Domain.Entities.Words;
using LexiLearn.Domain.Enums;

namespace LexiLearn.Domain.Entities.Questions;

public class Question : Auditable
{
    public QuestionType Type { get; set; }

    public long QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public long WordId { get; set; }
    public Word Word { get; set; }
}