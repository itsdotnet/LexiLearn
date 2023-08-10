using LexiLearn.Domain.Enums;
using LexiLearn.Service.DTOs.Words;
using LexiLearn.Service.Services;

namespace LexiLearn.Views.AdminView;

public class WordControl
{
    private readonly WordService wordService;

    public WordControl()
    {
        wordService = new WordService();
    }

    public async Task Start()
    {
        Console.WriteLine("Welcome to Word Management Console");

        while (true)
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Create Word");
            Console.WriteLine("2. Update Word");
            Console.WriteLine("3. Delete Word");
            Console.WriteLine("4. View All Words");
            Console.WriteLine("5. View Words by Category");
            Console.WriteLine("6. Search Words");
            Console.WriteLine("7. Exit");

            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateWordAsync();
                    break;
                case "2":
                    await UpdateWordAsync();
                    break;
                case "3":
                    await DeleteWordAsync();
                    break;
                case "4":
                    await ViewAllWordsAsync();
                    break;
                case "5":
                    await ViewWordsByCategoryAsync();
                    break;
                case "6":
                    await SearchWordsAsync();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    break;
            }
        }
    }

    private async Task CreateWordAsync()
    {
        Console.Write("Enter word text: ");
        var text = Console.ReadLine();

        Console.Write("Enter word translation: ");
        var translation = Console.ReadLine();

        Console.Write("Enter word definition: ");
        var definition = Console.ReadLine();

        Console.Write("Enter word pronunciation: ");
        var pronunciation = Console.ReadLine();

        Console.Write("Enter language (English/French/German, etc.): ");
        var languageStr = Console.ReadLine();
        Enum.TryParse<Language>(languageStr, out var language);

        Console.Write("Enter category ID: ");
        var categoryId = Convert.ToInt64(Console.ReadLine());

        var newWordDto = new WordCreationDto
        {
            Text = text,
            Translation = translation,
            Definition = definition,
            Pronunciation = pronunciation,
            Language = language,
            CategoryId = categoryId
        };

        var response = await wordService.CreateAsync(newWordDto);

        if (response.StatusCode == 201)
        {
            Console.WriteLine("Word created successfully!");
        }
        else
        {
            Console.WriteLine($"Error creating word: {response.Message}");
        }
    }

    private async Task UpdateWordAsync()
    {
        Console.Write("Enter word ID to update: ");
        var wordId = Convert.ToInt64(Console.ReadLine());

        var wordResponse = await wordService.GetByIdAsync(wordId);

        if (wordResponse.StatusCode != 200)
        {
            Console.WriteLine($"Error retrieving word: {wordResponse.Message}");
            return;
        }

        var word = wordResponse.Data;

        Console.Write("Enter new text: ");
        var newText = Console.ReadLine();

        Console.Write("Enter new translation: ");
        var newTranslation = Console.ReadLine();

        Console.Write("Enter new definition: ");
        var newDefinition = Console.ReadLine();

        Console.Write("Enter new pronunciation: ");
        var newPronunciation = Console.ReadLine();

        Console.Write("Enter new language (Uzbek/English): ");
        var newLanguageStr = Console.ReadLine();
        Enum.TryParse<Language>(newLanguageStr, out var newLanguage);

        Console.Write("Enter new category ID: ");
        var newCategoryId = Convert.ToInt64(Console.ReadLine());

        var updateWordDto = new WordUpdateDto
        {
            Id = wordId,
            Text = newText,
            Translation = newTranslation,
            Definition = newDefinition,
            Pronunciation = newPronunciation,
            Language = newLanguage,
            CategoryId = newCategoryId
        };

        var response = await wordService.UpdateAsync(updateWordDto);

        if (response.StatusCode == 200)
        {
            Console.WriteLine("Word updated successfully!");
        }
        else
        {
            Console.WriteLine($"Error updating word: {response.Message}");
        }
    }


    private async Task DeleteWordAsync()
    {
        Console.Write("Enter word ID to delete: ");
        var wordId = Convert.ToInt64(Console.ReadLine());

        var response = await wordService.DeleteAsync(wordId);

        if (response.StatusCode == 200)
        {
            Console.WriteLine("Word deleted successfully!");
        }
        else
        {
            Console.WriteLine($"Error deleting word: {response.Message}");
        }
    }

    private async Task ViewAllWordsAsync()
    {
        var response = await wordService.GetAllAsync();

        if (response.StatusCode == 200)
        {
            foreach (var word in response.Data)
            {
                Console.WriteLine($"Word ID: {word.Id}, Text: {word.Text}, Translation: {word.Translation}, Category ID: {word.CategoryId}");
            }
        }
        else
        {
            Console.WriteLine($"Error retrieving words: {response.Message}");
        }
    }

    private async Task ViewWordsByCategoryAsync()
    {
        Console.Write("Enter category ID: ");
        var categoryId = Convert.ToInt64(Console.ReadLine());

        var response = await wordService.GetWordsByCategoryAsync(categoryId);

        if (response.StatusCode == 200)
        {
            foreach (var word in response.Data)
            {
                Console.WriteLine($"Word ID: {word.Id}, Text: {word.Text}, Translation: {word.Translation}, Category ID: {word.CategoryId}");
            }
        }
        else
        {
            Console.WriteLine($"Error retrieving words: {response.Message}");
        }
    }

    private async Task SearchWordsAsync()
    {
        Console.Write("Enter search term: ");
        var searchTerm = Console.ReadLine();

        var response = await wordService.SearchWordsAsync(searchTerm);

        if (response.StatusCode == 200)
        {
            foreach (var word in response.Data)
            {
                Console.WriteLine($"Word ID: {word.Id}, Text: {word.Text}, Translation: {word.Translation}, Category ID: {word.CategoryId}");
            }
        }
        else
        {
            Console.WriteLine($"Error searching words: {response.Message}");
        }
    }
}
