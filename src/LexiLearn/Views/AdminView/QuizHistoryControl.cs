using LexiLearn.Domain.Entities.Quizzes;
using LexiLearn.Service.DTOs.Quizzes.QuizHistory;
using LexiLearn.Service.Helpers;
using LexiLearn.Service.Interfaces;

namespace LexiLearn.Service.Services;

public class QuizHistoryControl
{
    private readonly IQuizHistoryService quizHistoryService;

    public QuizHistoryControl()
    {
        this.quizHistoryService = new QuizHistoryService();
    }

    public async Task StartAsync()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("Quiz History Control");
            Console.WriteLine("\n1. Create Quiz History");
            Console.WriteLine("2. Get Quiz History by User ID");
            Console.WriteLine("3. Get Quiz History by Quiz ID");
            Console.WriteLine("4. Delete Quiz History");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateQuizHistory();
                    break;
                case "2":
                    await GetQuizHistoryByUserId();
                    break;
                case "3":
                    await GetQuizHistoryByQuizId();
                    break;
                case "4":
                    await DeleteQuizHistory();
                    break;
                case "5":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private async Task CreateQuizHistory()
    {
        Console.Write("Enter User ID: ");
        long userId = Convert.ToInt64(Console.ReadLine());

        Console.Write("Enter Quiz ID: ");
        long quizId = Convert.ToInt64(Console.ReadLine());

        Console.Write("Enter score: ");
        long score = Convert.ToInt64(Console.ReadLine());

        var creationDto = new QuizHistoryCreationDto
        {
            UserId = userId,
            QuizId = quizId,
            Score = score
        };

        var response = await quizHistoryService.CreateAsync(creationDto);

        Console.WriteLine(response.Message);
    }

    private async Task GetQuizHistoryByUserId()
    {
        Console.Write("Enter User ID: ");
        long userId = Convert.ToInt64(Console.ReadLine());

        var response = await quizHistoryService.GetQuizHistoryByUserIdAsync(userId);

        PrintQuizHistory(response);
    }

    private async Task GetQuizHistoryByQuizId()
    {
        Console.Write("Enter Quiz ID: ");
        long quizId = Convert.ToInt64(Console.ReadLine());

        var response = await quizHistoryService.GetQuizHistoryByQuizIdAsync(quizId);

        PrintQuizHistory(response);
    }

    private async Task DeleteQuizHistory()
    {
        Console.Write("Enter Quiz History ID: ");
        long id = Convert.ToInt64(Console.ReadLine());

        var response = await quizHistoryService.DeleteAsync(id);

        Console.WriteLine(response.Message);
    }

    private void PrintQuizHistory(Response<IEnumerable<QuizHistory>> response)
    {
        if (response.StatusCode == 200)
        {
            foreach (var quizHistory in response.Data)
            {
                Console.WriteLine($"Quiz History ID: {quizHistory.Id}");
                Console.WriteLine($"User ID: {quizHistory.UserId}");
                Console.WriteLine($"Quiz ID: {quizHistory.QuizId}");
                Console.WriteLine("------------------------------");
            }
        }
        else
        {
            Console.WriteLine(response.Message);
        }
    }
}