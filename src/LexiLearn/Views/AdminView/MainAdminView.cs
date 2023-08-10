using LexiLearn.Service.Services;
using LexiLearn.Views.AdminView;
using System;

namespace LexiLearn.AdminConsole;

public class MainAdminView
{
    private readonly WordCategoryControl wordCategoryControl;
    private readonly QuizCategoryControl quizCategoryControl;
    private readonly QuestionControl questionControl;
    private readonly QuizHistoryControl quizHistoryControl;
    private readonly UserControl userControl;
    private readonly WordControl wordControl;
    private readonly QuizControl quizControl;

    public MainAdminView()
    {
        wordCategoryControl = new WordCategoryControl();
        quizCategoryControl = new QuizCategoryControl();
        questionControl = new QuestionControl();
        quizHistoryControl = new QuizHistoryControl();
        userControl = new UserControl();
        wordControl = new WordControl();
        quizControl = new QuizControl();
    }

    public async Task StartAsync()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("Main Admin View");
            Console.WriteLine("\n1. Manage Word Categories");
            Console.WriteLine("2. Manage Quiz Categories");
            Console.WriteLine("3. Manage Questions");
            Console.WriteLine("4. Manage Quiz History");
            Console.WriteLine("5. Manage Users");
            Console.WriteLine("6. Manage Words");
            Console.WriteLine("7. Manage Quizzes");
            Console.WriteLine("8. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ManageWordCategories();
                    break;
                case "2":
                    await ManageQuizCategories();
                    break;
                case "3":
                    await ManageQuestions();
                    break;
                case "4":
                    await ManageQuizHistory();
                    break;
                case "5":
                    await ManageUsers();
                    break;
                case "6":
                    await ManageWords();
                    break;
                case "7":
                    await ManageQuizzes();
                    break;
                case "8":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private async Task ManageWordCategories()
    {
        await wordCategoryControl.StartAsync();
    }

    private async Task ManageQuizCategories()
    {
        await quizCategoryControl.StartAsync();
    }

    private async Task ManageQuestions()
    {
        await questionControl.StartAsync();
    }

    private async Task ManageQuizHistory()
    {
        await quizHistoryControl.StartAsync();
    }

    private async Task ManageUsers()
    {
        await userControl.StartAsync();
    }

    private async Task ManageWords()
    {
        await wordControl.StartAsync();
    }

    private async Task ManageQuizzes()
    {
        await quizControl.StartAsync();
    }
}
