using LexiLearn.Domain.Commons;

namespace LexiLearn.Domain.Entities.Categories;

public class WordCategory : Auditable
{
    public string Title { get; set; }

    public string Description { get; set; }
}