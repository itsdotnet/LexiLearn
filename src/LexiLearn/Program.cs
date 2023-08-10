using LexiLearn.Views.StartView;

namespace LexiLearn
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            MainView mainView = new MainView();
            await mainView.StartAsync();
        }
    }
}