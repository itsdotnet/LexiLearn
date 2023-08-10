using LexiLearn.Service.DTOs.Categories;
using LexiLearn.Service.Interfaces;

namespace LexiLearn.Service.Services;

public class QuizCategoryControl
{
    private readonly IQuizCategoryService quizCategoryService;

    public QuizCategoryControl( )
    {
        this.quizCategoryService = new QuizCategoryService();
    }

    public async Task StartAsync()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("Quiz Category Control");
            Console.WriteLine("1. Create Quiz Category");
            Console.WriteLine("2. Update Quiz Category");
            Console.WriteLine("3. Delete Quiz Category");
            Console.WriteLine("4. Get Quiz Category by ID");
            Console.WriteLine("5. Get All Quiz Categories");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateQuizCategory();
                    break;
                case "2":
                    await UpdateQuizCategory();
                    break;
                case "3":
                    await DeleteQuizCategory();
                    break;
                case "4":
                    await GetQuizCategoryById();
                    break;
                case "5":
                    await GetAllQuizCategories();
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

    private async Task CreateQuizCategory()
    {
        Console.Write("Enter Quiz Category Name: ");
        string categoryName = Console.ReadLine();

        Console.Write("Enter Quiz Category Description: ");
        string categoryDescription = Console.ReadLine();

        var creationDto = new CategoryCreationDto
        {
            Title = categoryName,
            Description = categoryDescription
        };

        var response = await quizCategoryService.CreateAsync(creationDto);

        Console.WriteLine(response.Message);
    }

    private async Task UpdateQuizCategory()
    {
        Console.Write("Enter Quiz Category ID: ");
        long categoryId = Convert.ToInt64(Console.ReadLine());

        Console.Write("Enter New Quiz Category Name: ");
        string newCategoryName = Console.ReadLine();

        Console.Write("Enter Quiz Category Description: ");
        string categoryDescription = Console.ReadLine();

        var updateDto = new CategoryUpdateDto
        {
            Id = categoryId,
            Title = newCategoryName,
            Description = categoryDescription
        };

        var response = await quizCategoryService.UpdateAsync(updateDto);

        Console.WriteLine(response.Message);
    }

    private async Task DeleteQuizCategory()
    {
        Console.Write("Enter Quiz Category ID: ");
        long categoryId = Convert.ToInt64(Console.ReadLine());

        var response = await quizCategoryService.DeleteAsync(categoryId);

        Console.WriteLine(response.Message);
    }

    private async Task GetQuizCategoryById()
    {
        Console.Write("Enter Quiz Category ID: ");
        long categoryId = Convert.ToInt64(Console.ReadLine());

        var response = await quizCategoryService.GetByIdAsync(categoryId);

        if (response.StatusCode == 200)
        {
            Console.WriteLine($"Quiz Category ID: {response.Data.Id}");
            Console.WriteLine($"Quiz Category Name: {response.Data.Title}");
            Console.WriteLine($"Word Category Description: {response.Data.Description}");
        }
        else
        {
            Console.WriteLine(response.Message);
        }
    }

    private async Task GetAllQuizCategories()
    {
        var response = await quizCategoryService.GetAllAsync();

        if (response.StatusCode == 200)
        {
            foreach (var category in response.Data)
            {
                Console.WriteLine($"Quiz Category ID: {category.Id}");
                Console.WriteLine($"Quiz Category Name: {category.Title}");
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