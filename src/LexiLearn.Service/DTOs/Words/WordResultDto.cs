using LexiLearn.Domain.Enums;

namespace LexiLearn.Service.DTOs.Words;

public class WordResultDto
{
    public long Id { get; set; }

    public string Text { get; set; }

    public string Translation { get; set; }

    public string Definition { get; set; }

    public string Pronunciation { get; set; }

    public Language Language { get; set; }

    public long CategoryId { get; set; }
}