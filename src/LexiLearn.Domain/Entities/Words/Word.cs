using LexiLearn.Domain.Commons;
using LexiLearn.Domain.Entities.Categories;
using LexiLearn.Domain.Enums;

namespace LexiLearn.Domain.Entities.Words;

public class Word : Auditable
{
    public string Text { get; set; }

    public string Translation { get; set; }

    public string Definition { get; set; }

    public string Pronunciation { get; set; }

    public Language Language { get; set; }
    
    public long CategoryId { get; set; }
    public WordCategory Category { get; set; }
}