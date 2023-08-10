using LexiLearn.Domain.Entity.User;
using LexiLearn.Domain.Enums;
using LexiLearn.Domain.Services;
using LexiLearn.Service.Interfaces;
using LexiLearn.Service.Services;

namespace LexiLearn.Views.UserView;

public class MainUserUI
{
    private IQuestionService questionService;
    private IQuizService quizService;
    private IQuizHistoryService quizHistoryService;
    private IUserService userService;
    private IWordService wordService;
    private ProfileControl profile;
    private User currentUser;

    public MainUserUI(User user)
    {
        this.currentUser = user;
        this.questionService = new QuestionService();
        this.quizService = new QuizService();
        this.quizHistoryService = new QuizHistoryService();
        this.userService = new UserService();
        this.wordService = new WordService();
        profile = new ProfileControl(currentUser);
    }

    public async Task StartAsync()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("Welcome to LexiLearn");
            Console.WriteLine("\n1. See all words");
            Console.WriteLine("2. See all scores");
            Console.WriteLine("3. Start quiz");
            Console.WriteLine("4. Profile settings");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await SeeAllWordsAsync();
                    break;
                case "2":
                    await SeeAllScoresAsync();
                    break;
                case "3":
                    await StartQuizAsync();
                    break;
                case "4":
                    await profile.StartAsync();
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
    private async Task SeeAllWordsAsync()
    {
        var response = await wordService.GetAllAsync();

        if (response.Data is null)
        {
            Console.Clear();
            Console.WriteLine("Sorry but there is no words.");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Uzbek                 English");
            Console.WriteLine("--------------------------------");

            foreach (var word in response.Data)
            {
                if (word.Language == Language.English)
                    Console.WriteLine($"{word.Translation,-25}{word.Text}");
                else
                    Console.WriteLine($"{word.Text,-25} {word.Translation}");
            }
        }
    }

    public async Task SeeAllScoresAsync()
    {
        var response = await quizHistoryService.GetQuizHistoryByUserIdAsync(currentUser.Id);
        var allHistory = response.Data;
        if (allHistory is null)
        {
            Console.Clear();
            Console.WriteLine("You don't participation on quizzes!");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Quiz Name               Score");
            Console.WriteLine("--------------------------------");

            foreach (var history in allHistory)
                Console.WriteLine($"{history.Quiz.Title,-25}{history.Score}");

            Console.WriteLine($"All score : {allHistory.Select(z => z.Score).Sum()}");
        }
    }

    public async Task StartQuizAsync()
    {
        var response = await quizService.GetAllAsync();

        if (response.StatusCode != 200)
        {
            Console.Clear();
            Console.WriteLine("Quizzes not added yet please wait.");
            return;
        }

        var allQuizzes = response.Data;

        Console.Clear();
        Console.WriteLine("Quiz Name               Quiz Id");
        Console.WriteLine("--------------------------------");

        foreach (var quiz in allQuizzes)
        {
            Console.WriteLine($"{quiz.Title,-25}{quiz.Id}");
        }
        Console.Write("\nEnter Quiz ID for solve :");

        long quizId = Convert.ToInt64(Console.ReadLine());

        if (!allQuizzes.Select(x => x.Id).Contains(quizId))
        {
            Console.Clear();
            Console.WriteLine("Entered invalid id.");
            return;
        }

        var allQuestion = (await questionService.GetQuestionsByQuizIdAsync(quizId)).Data;
        double scorePresent = 100 / allQuestion.Count();
        double scoreSum = 0;

        foreach (var question in allQuestion)
        {
            var que = await questionService.PrintQuestionAsync(question.Id);

            Console.Write("Answer: ");
            var answer = Console.ReadLine();
            if (que == answer.ToLower().Trim())
            {
                Console.WriteLine("Great.");
                scoreSum += scorePresent;
            }
            else
            {
                Console.WriteLine($"Wrong answer. True : {que}");
                scoreSum -= scorePresent;
            }
        }

        int lastResult = scoreSum > 99 ? 100 : Convert.ToInt32(scoreSum);

        Console.WriteLine($"You finishid Quiz and earned {scoreSum} score");
        await userService.UpdateScoreAsync(currentUser.Id, lastResult);
    }
}