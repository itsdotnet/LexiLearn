using LexiLearn.Domain.Enums;

namespace LexiLearn.Service.DTOs.Questions;

public class QuestionResultDto
{
    public long Id { get; set; }

    public QuestionType Type { get; set; }

    public long WordId { get; set; }
}