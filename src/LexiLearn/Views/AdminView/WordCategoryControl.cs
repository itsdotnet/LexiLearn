using LexiLearn.Service.DTOs.Categories;
using LexiLearn.Service.Interfaces;

namespace LexiLearn.Service.Services;

public class WordCategoryControl
{
    private readonly IWordCategoryService wordCategoryService;

    public WordCategoryControl()
    {
        this.wordCategoryService = new WordCategoryService();
    }

    public async Task StartAsync()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("Word Category Control");
            Console.WriteLine("1. Create Word Category");
            Console.WriteLine("2. Update Word Category");
            Console.WriteLine("3. Delete Word Category");
            Console.WriteLine("4. Get Word Category by ID");
            Console.WriteLine("5. Get All Word Categories");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateWordCategory();
                    break;
                case "2":
                    await UpdateWordCategory();
                    break;
                case "3":
                    await DeleteWordCategory();
                    break;
                case "4":
                    await GetWordCategoryById();
                    break;
                case "5":
                    await GetAllWordCategories();
                    break;
                case "6":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private async Task CreateWordCategory()
    {
        Console.Write("Enter Word Category Name: ");
        string categoryName = Console.ReadLine();

        Console.Write("Enter Word Category Description: ");
        string categoryDescription = Console.ReadLine();

        var creationDto = new CategoryCreationDto
        {
            Title = categoryName,
            Description = categoryDescription
        };

        var response = await wordCategoryService.CreateAsync(creationDto);

        Console.WriteLine(response.Message);
    }

    private async Task UpdateWordCategory()
    {
        Console.Write("Enter Word Category ID: ");
        long categoryId = Convert.ToInt64(Console.ReadLine());

        Console.Write("Enter New Word Category Name: ");
        string newCategoryName = Console.ReadLine();

        Console.Write("Enter Word Category Description: ");
        string categoryDescription = Console.ReadLine();

        var updateDto = new CategoryUpdateDto
        {
            Id = categoryId,
            Title = newCategoryName,
            Description = categoryDescription
        };

        var response = await wordCategoryService.UpdateAsync(updateDto);

        Console.WriteLine(response.Message);
    }

    private async Task DeleteWordCategory()
    {
        Console.Write("Enter Word Category ID: ");
        long categoryId = Convert.ToInt64(Console.ReadLine());

        var response = await wordCategoryService.DeleteAsync(categoryId);

        Console.WriteLine(response.Message);
    }

    private async Task GetWordCategoryById()
    {
        Console.Write("Enter Word Category ID: ");
        long categoryId = Convert.ToInt64(Console.ReadLine());

        var response = await wordCategoryService.GetByIdAsync(categoryId);

        if (response.StatusCode == 200)
        {
            Console.WriteLine($"Word Category ID: {response.Data.Id}");
            Console.WriteLine($"Word Category Name: {response.Data.Title}");
            Console.WriteLine($"Word Category Description: {response.Data.Description}");
        }
        else
        {
            Console.WriteLine(response.Message);
        }
    }

    private async Task GetAllWordCategories()
    {
        var response = await wordCategoryService.GetAllAsync();

        if (response.StatusCode == 200)
        {
            foreach (var category in response.Data)
            {
                Console.WriteLine($"Word Category ID: {category.Id}");
                Console.WriteLine($"Word Category Name: {category.Title}");
                Console.WriteLine($"Word Category Description: {category.Description}");
                Console.WriteLine("------------------------------");
            }
        }
        else
        {
            Console.WriteLine(response.Message);
        }
    }
}
