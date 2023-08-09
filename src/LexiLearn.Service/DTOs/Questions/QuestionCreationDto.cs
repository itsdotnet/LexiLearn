using LexiLearn.Domain.Enums;

namespace LexiLearn.Service.DTOs.Questions;

public class QuestionCreationDto
{
    public QuestionType Type { get; set; }

    public long WordId { get; set; }
}