using LexiLearn.Domain.Enums;
using LexiLearn.Service.DTOs.Quizzes;
using LexiLearn.Service.Interfaces;
using LexiLearn.Service.Services;

namespace LexiLearn.Views.AdminView;

public class QuizControl
{
    private readonly IQuizService quizService;

    public QuizControl()
    {
        quizService = new QuizService();
    }

    public async Task StartAsync()
    {
        Console.WriteLine("Welcome to Quiz Management Console");

        while (true)
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Create Quiz");
            Console.WriteLine("2. Update Quiz");
            Console.WriteLine("3. Delete Quiz");
            Console.WriteLine("4. View All Quizzes");
            Console.WriteLine("5. View Quizzes by Category");
            Console.WriteLine("6. View Quizzes by Level");
            Console.WriteLine("7. Search Quizzes");
            Console.WriteLine("8. Exit");

            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateQuiz();
                    break;
                case "2":
                    await UpdateQuiz();
                    break;
                case "3":
                    await DeleteQuiz();
                    break;
                case "4":
                    await ViewAllQuizzes();
                    break;
                case "5":
                    await ViewQuizzesByCategory();
                    break;
                case "6":
                    await ViewQuizzesByLevel();
                    break;
                case "7":
                    await SearchQuizzes();
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    break;
            }
        }
    }

    private async Task CreateQuiz()
    {
        var newQuizDto = new QuizCreationDto();

        Console.Write("Enter title: ");
        newQuizDto.Title = Console.ReadLine();

        Console.Write("Enter description: ");
        newQuizDto.Description = Console.ReadLine();

        Console.Write("Enter category ID: ");
        newQuizDto.CategoryId = Convert.ToInt64(Console.ReadLine());

        Console.Write("Enter level (1 for Easy, 2 for Medium, 3 for Hard): ");
        if (Enum.TryParse(Console.ReadLine(), out QuizLevel level))
        {
            newQuizDto.Level = level;

            Console.Write("Enter deadline (in days): ");
            newQuizDto.Deadline = Convert.ToInt32(Console.ReadLine());

            var response = await quizService.CreateAsync(newQuizDto);

            if (response.StatusCode == 200)
            {
                Console.WriteLine("Quiz created successfully!");
            }
            else
            {
                Console.WriteLine($"Error creating quiz: {response.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid level selection.");
        }
    }

    private async Task UpdateQuiz()
    {
        var newQuizDto = new QuizUpdateDto();

        Console.Write("Enter ID: ");
        newQuizDto.Id = Convert.ToInt64(Console.ReadLine());

        Console.Write("Enter title: ");
        newQuizDto.Title = Console.ReadLine();

        Console.Write("Enter description: ");
        newQuizDto.Description = Console.ReadLine();

        Console.Write("Enter category ID: ");
        newQuizDto.CategoryId = Convert.ToInt64(Console.ReadLine());

        Console.Write("Enter level (1 for Easy, 2 for Medium, 3 for Hard): ");
        if (Enum.TryParse(Console.ReadLine(), out QuizLevel level))
        {
            newQuizDto.Level = level;

            Console.Write("Enter deadline (in days): ");
            newQuizDto.Deadline = Convert.ToInt32(Console.ReadLine());

            var response = await quizService.UpdateAsync(newQuizDto);

            if (response.StatusCode == 200)
            {
                Console.WriteLine("Quiz updated successfully!");
            }
            else
            {
                Console.WriteLine($"Error updating quiz: {response.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid level selection.");
        }
    }

    private async Task DeleteQuiz()
    {
        Console.Write("Enter quiz ID: ");
        var quizId = Convert.ToInt64(Console.ReadLine());

        var response = await quizService.DeleteAsync(quizId);

        if (response.StatusCode == 200)
        {
            Console.WriteLine("Quiz updated successfully!");
        }
        else
        {
            Console.WriteLine($"Error updating quiz: {response.Message}");
        }
    }

    private async Task ViewAllQuizzes()
    {
        var response = await quizService.GetAllAsync();

        if (response.StatusCode == 200)
        {
            Console.WriteLine("All Quizzes:");
            foreach (var quiz in response.Data)
            {
                Console.WriteLine($"Quiz ID: {quiz.Id}, Title: {quiz.Title}, Description: {quiz.Description}");
            }
        }
        else
        {
            Console.WriteLine($"Error retrieving quizzes: {response.Message}");
        }
    }

    private async Task ViewQuizzesByCategory()
    {
        Console.Write("Enter category ID: ");
        var categoryId = Convert.ToInt64(Console.ReadLine());

        var response = await quizService.GetQuizzesByCategoryAsync(categoryId);

        if (response.StatusCode == 200)
        {
            Console.WriteLine($"Quizzes in Category {categoryId}:");
            foreach (var quiz in response.Data)
            {
                Console.WriteLine($"Quiz ID: {quiz.Id}, Title: {quiz.Title}, Description: {quiz.Description}");
            }
        }
        else
        {
            Console.WriteLine($"Error retrieving quizzes: {response.Message}");
        }
    }

    private async Task ViewQuizzesByLevel()
    {
        Console.Write("Enter level (1 for Easy, 2 for Medium, 3 for Hard, 4 for Impossible): ");
        if (Enum.TryParse(Console.ReadLine(), out QuizLevel level))
        {
            var response = await quizService.GetQuizzesByLevelAsync(level);

            if (response.StatusCode == 200)
            {
                Console.WriteLine($"Quizzes in Level {level}:");
                foreach (var quiz in response.Data)
                {
                    Console.WriteLine($"Quiz ID: {quiz.Id}, Title: {quiz.Title}, Description: {quiz.Description}");
                }
            }
            else
            {
                Console.WriteLine($"Error retrieving quizzes: {response.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid level selection.");
        }
    }

    private async Task SearchQuizzes()
    {
        Console.Write("Enter search term: ");
        var searchTerm = Console.ReadLine();

        var response = await quizService.SearchQuizzesAsync(searchTerm);

        if (response.StatusCode == 200)
        {
            Console.WriteLine($"Quizzes matching search term '{searchTerm}':");
            foreach (var quiz in response.Data)
            {
                Console.WriteLine($"Quiz ID: {quiz.Id}, Title: {quiz.Title}, Description: {quiz.Description}");
            }
        }
        else
        {
            Console.WriteLine($"Error retrieving quizzes: {response.Message}");
        }
    }
}
